import React from 'react';
import * as styles from './ParkingComponent.scss';
import ParkingSpotActions from '../../reflux/actions/ParkingSpotsActions';
import ParkingSpotsStore from '../../reflux/stores/ParkingSpotsStore';
import ParkingSpotsActions from '../../reflux/actions/ParkingSpotsActions';
import {
    getFirstParkingRow,
    getSecondParkingRow,
    getFirstParkingRowCircles,
    getHorizontalLine, getSecondParkingRowCircles, getVerticalLine, getArrowData, getLabels,
    getFormattedTime
} from './parkingComponentHelpers';
import * as d3 from 'd3';

class ParkingComponent extends React.PureComponent {
    static displayName = 'ParkingComponent';

    constructor(props) {
        ParkingSpotsActions.setParkingInfoPolling();
        super(props);

        this.state = {
            reservingBlocked: ParkingSpotsStore.wasSpotReserved,
            currentMessage: 'Забронируйте любое свободное место',
            content: {}
        };
    }

    drawCircles = (svgContainer, jsonCircles) => {
        const rectangles = svgContainer.selectAll("rect");
        svgContainer.selectAll("circle")
            .data(jsonCircles)
            .enter()
            .append("circle")
            .attr("cx", function (d) {
                return d.x_axis;
            })
            .attr("cy", function (d) {
                return d.y_axis;
            })
            .attr("r", function (d) {
                return d.radius;
            })
            .style("fill", function (d) {
                return d.color;
            })
            .on("mouseover", function (d) {
                rectangles.filter(function (dInner) {
                    return d.id === dInner.id
                }).style("fill", "#293740")
            })
            .on("mouseout", function (d) {
                rectangles.filter(function (dInner) {
                    return d.id === dInner.id
                }).style("fill", "transparent");
            })
            .on("click", (d) => {
                d3.event.stopPropagation();
                this.reserve(d.id, d3.event);
            })
            .style("cursor", "pointer");
    };

    drawSpotsLabels = (svgContainer, jsonLabels) => {
        const rectangles = svgContainer.selectAll("rect");
        svgContainer.selectAll("circle")
            .data(jsonLabels)
            .enter()
            .append("text")
            .attr("x", function (d) {
                return d.x_axis;
            })
            .attr("y", function (d) {
                return d.y_axis;
            })
            .style("fill", function (d) {
                return 'eee';
            })
            .on("mouseover", function (d) {
                rectangles.filter(function (dInner) {
                    return d.id === dInner.id
                }).style("fill", "#293740")
            })
            .on("mouseout", function (d) {
                rectangles.filter(function (dInner) {
                    return d.id === dInner.id
                }).style("fill", "transparent");
            })
            .on("click", (d) => {
                d3.event.stopPropagation();
                this.reserve(d.id, d3.event);
            })
            .attr("font-size", "3px")
            .style("cursor", "pointer")
            .text((d) => {
                return ParkingSpotsStore.content[d.id].name
            });
    };

    componentWillUpdate(nextProps, nextState) {
        if (!this.state.content[0] && nextState.content[0]) {
            const jsonRectangles = getFirstParkingRow().concat(getSecondParkingRow());
            const jsonCircles = getFirstParkingRowCircles(nextState.content).concat(getSecondParkingRowCircles(nextState.content));
            const jsonLabels = getLabels();

            const rectangles = this.svgContainer.selectAll("rect")
                .data(jsonRectangles)
                .enter()
                .append("rect")
                .on("click", (d) => {
                    d3.event.stopPropagation();
                    this.reserve(d.id, d3.event);
                });

            const horizontalLineData = getHorizontalLine();
            const verticalLineData = getVerticalLine();

            this.drawSpotsLabels(this.svgContainer, jsonLabels);

            this.drawCircles(this.svgContainer, jsonCircles);
            this.svgContainer.append("line")
                .attr("x1", function (d) {
                    return horizontalLineData[0].x;
                })
                .attr("y1", function (d) {
                    return horizontalLineData[0].y;
                })
                .attr("x2", function (d) {
                    return horizontalLineData[1].x;
                })
                .attr("y2", function (d) {
                    return horizontalLineData[1].y;
                })
                .attr("stroke-width", 0.5)
                .attr("stroke", "white")
                .attr("stroke-dasharray", "5%");

            this.svgContainer.append("line")
                .attr("x1", function (d) {
                    return verticalLineData[0].x;
                })
                .attr("y1", function (d) {
                    return verticalLineData[0].y;
                })
                .attr("x2", function (d) {
                    return verticalLineData[1].x;
                })
                .attr("y2", function (d) {
                    return verticalLineData[1].y;
                })
                .attr("stroke-width", 0.5)
                .attr("stroke-dasharray", "5%")
                .attr("stroke", "white");


            const rectangleAttributes = rectangles
                .attr("x", function (d) {
                    return d.x_axis;
                })
                .attr("y", function (d) {
                    return d.y_axis;
                })
                .attr("height", function (d) {
                    return d.height;
                })
                .attr("width", function (d) {
                    return d.width;
                })
                .style("fill", function (d) {
                    return d.color;
                })
                .on("mouseover", function (d) {
                    d3.select(this).style("fill", "#293740")
                })
                .on("mouseout", function (d) {
                    d3.select(this).style("fill", d.color);
                })
                .style("z-index", 1)
                .attr("stroke-width", function (d) {
                    return d.noBorder ? 0 : 0.5;
                })
                .attr("stroke", function (d) {
                    return d.noBorder ? 'none' : 'white';
                })
                .style("cursor", "pointer");
        }
    }

    drawArrow = (data) => {
        const line = this.svgContainer.append('polyline');
        let points = '';

        data.forEach((point, key) => {
            points = points + (key !== 0 ? ',' : '') + point.x + ',' + point.y;
        });
        line
            .attr('points', points)
            .attr("stroke-width", 0.5)
            .attr("stroke", "blue")
            .style("fill", "none");
        // return line;
    };

    onSpotReserve = () => {
        if (ParkingSpotsStore.reservedSpotId || ParkingSpotsStore.reservedSpotId === 0) {
            this.svgContainer.selectAll("circle")
                .filter(function (d) {
                    return d.id === ParkingSpotsStore.reservedSpotId
                })
                .style("fill", function () {
                    return 'grey';
                });
            const arrowData = getArrowData(ParkingSpotsStore.reservedSpotId);
            this.drawArrow(arrowData);
        } else {
            this.setState({currentMessage: `Забронируйте любое свободное место`});
            this.svgContainer.selectAll('polyline').remove();
        }
    };

    componentDidMount() {
        //setLoader();
        this.svgContainer = d3.select("#parking-component").append("svg")
            .attr("width", '100%')
            .attr("height", '100%')
            .attr('viewBox', "0 0 100 100")
            .attr('preserveAspectRatio', "none");
        ParkingSpotsStore.addCurrentUserSpotReserveListener(this.toggleReservingBlocking);
        ParkingSpotsStore.addReserveListener(this.onSpotReserve);
        ParkingSpotsStore.addContentChangeListener(this.changeContent);
    }

    componentWillUnmount() {
        ParkingSpotsActions.stopParkingInfoPolling();
        ParkingSpotsStore.removeCurrentUserSpotReserveListener(this.toggleReservingBlocking);
        ParkingSpotsStore.removeReserveListener(this.onSpotReserve);
        ParkingSpotsStore.removeContentChangeListener(this.changeContent);
    }

    changeContent = () => {
        this.setState({content: ParkingSpotsStore.content});
        const reservedSpot = ParkingSpotsStore.content[ParkingSpotsStore.reservedSpotId];
        if (reservedSpot && reservedSpot.remainingTime) {
            const timeRemaining = getFormattedTime(+reservedSpot.remainingTime);
            this.setState({
                currentMessage: `Место ${ParkingSpotsStore.content[ParkingSpotsStore.reservedSpotId].name} забронировано.
             До отмены бронирования осталось ${timeRemaining.value} ${timeRemaining.unit}`
            });
            const arrowData = getArrowData(ParkingSpotsStore.reservedSpotId);
            this.drawArrow(arrowData);
        }
        this.svgContainer.selectAll("circle")
            .data(getFirstParkingRowCircles(ParkingSpotsStore.content).concat(getSecondParkingRowCircles(ParkingSpotsStore.content)))
            .style("fill", function (d) {
                return d.color;
            });
    };

    toggleReservingBlocking = () => {
        this.setState({reservingBlocked: ParkingSpotsStore.wasSpotReserved});
    };

    reserve = (id, event) => {
        event.preventDefault();
        this.setState({reservingBlocked: true});
        ParkingSpotActions.reserveSpot(id);
    };

    render() {
        const {reservingBlocked, currentMessage} = this.state;
        return (
            <div className={styles.container}>
                <div className={styles.messageContainer}>
                    {currentMessage}
                </div>
                <div className={styles.parking} id="parking-component">
                    {reservingBlocked ? <div className={styles.overlay} /> : null}
                </div>
            </div>
        );
    }
}

export default ParkingComponent;