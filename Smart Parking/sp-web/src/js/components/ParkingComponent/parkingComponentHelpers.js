import React from 'react';
import {
    PARKING_SPOTS_COUNT,
    FIRST_PARKING_ROW_SPOTS_COUNT,
    SECOND_PARKING_ROW_SPOTS_COUNT, CIRCLE_RADIUS, RESERVATION_TIMEOUT
} from '../../reflux/constants/Constants';
import ParkingSpot from '../ParkingComponent/ParkingSpot/ParkingSpot.react';
import RoadCell from '../ParkingComponent/RoadCell/RoadCell.react';

// /**
//  * Функция для получния JSX-компонентов мест парковки в первом (верхнем) ряду
//  * @function getFirstParkingRow
//  * @param {Array} content - данные о парковке из Store
//  * @param {Function} onClick - обработчик клика на парковочное место
//  * @return {Array} spots - массив с JSX-компонентами парковочных мест
//  * */
// export function getFirstParkingRow(content, onClick) {
//     const spots = [];
//     let noBorder = false;
//     for (let index = 0; index < FIRST_PARKING_ROW_SPOTS_COUNT; ++index) {
//         const currentContent = content[index];
//         if (index === FIRST_PARKING_ROW_SPOTS_COUNT - 1) { noBorder = true; }
//         spots.push(
//             <ParkingSpot
//                 key={index}
//                 noBorder={noBorder}
//                 onSpotClick={onClick.bind(null, currentContent ? currentContent.id : null)}
//                 occupied={currentContent ? currentContent.occupied : null}
//                 reserved={currentContent ? currentContent.reserved : null}
//                 text={currentContent ? currentContent.name : null}
//             />
//         );
//     }
//     return spots;
// }

export function getFormattedTime(reservationTime) {
    if (reservationTime > 60000) {
        return {value: Math.ceil(reservationTime / 60000), unit: 'мин'};
    } else if (reservationTime > 0) {
        return {value: Math.ceil(reservationTime / 1000), unit: 'с'};
    } else {
        return {value: 0, unit: 'с'};
    }
}

export function getLabels() {
    const labels = [];

    for (let index = 0; index < FIRST_PARKING_ROW_SPOTS_COUNT; ++index) {
        labels.push(
            {
                id: index,
                "x_axis": (index * 100 / FIRST_PARKING_ROW_SPOTS_COUNT) + 40 / FIRST_PARKING_ROW_SPOTS_COUNT + '%',
                "y_axis": '6%'
            }
        );
    }

    for (let index = 0; index < SECOND_PARKING_ROW_SPOTS_COUNT / 2; ++index) {
        labels.push(
            {
                id: FIRST_PARKING_ROW_SPOTS_COUNT + index,
                "x_axis": (index * 100 / FIRST_PARKING_ROW_SPOTS_COUNT) + 40 / FIRST_PARKING_ROW_SPOTS_COUNT + '%',
                "y_axis": '81%'
            }
        );
    }
    for (let index = SECOND_PARKING_ROW_SPOTS_COUNT / 2; index < SECOND_PARKING_ROW_SPOTS_COUNT; ++index) {
        labels.push(
            {
                id: FIRST_PARKING_ROW_SPOTS_COUNT + index,
                "x_axis": ((index + 2) * 100 / FIRST_PARKING_ROW_SPOTS_COUNT) + 40 / FIRST_PARKING_ROW_SPOTS_COUNT + '%',
                "y_axis": '81%'
            }
        );
    }

    return labels;
}

/**
 * Функция для получния JSX-компонентов мест парковки в первом (верхнем) ряду
 * @function getFirstParkingRow
 * @return {Array} spots - массив с JSX-компонентами парковочных мест
 * */
export function getFirstParkingRow() {
    const spots = [];
    for (let index = 0; index < FIRST_PARKING_ROW_SPOTS_COUNT; ++index) {
        spots.push(
            {
                id: index,
                "x_axis": index * 100 / FIRST_PARKING_ROW_SPOTS_COUNT + '%',
                "y_axis": 0,
                "height": '25%',
                "width": 100 / FIRST_PARKING_ROW_SPOTS_COUNT + '%',
                color: 'transparent'
            }
        );
    }
    return spots;
}

/**
 * Функция для получния JSX-компонентов мест парковки в первом (верхнем) ряду
 * @function getSecondParkingRow
 * @return {Array} spots - массив с JSX-компонентами парковочных мест
 * */
export function getSecondParkingRow() {
    const spots = [];
    for (let index = 0; index < SECOND_PARKING_ROW_SPOTS_COUNT / 2; ++index) {
        spots.push(
            {
                id: FIRST_PARKING_ROW_SPOTS_COUNT + index,
                "x_axis": index * 100 / FIRST_PARKING_ROW_SPOTS_COUNT + '%',
                "y_axis": '75%',
                "height": '25%',
                "width": 100 / FIRST_PARKING_ROW_SPOTS_COUNT + '%',
                color: 'transparent'
            }
        );
    }
    for (let index = SECOND_PARKING_ROW_SPOTS_COUNT / 2; index < SECOND_PARKING_ROW_SPOTS_COUNT; ++index) {
        spots.push(
            {
                id: FIRST_PARKING_ROW_SPOTS_COUNT + index,
                "x_axis": (index + 2) * 100 / FIRST_PARKING_ROW_SPOTS_COUNT + '%',
                "y_axis": '75%',
                "height": '25%',
                "width": 100 / FIRST_PARKING_ROW_SPOTS_COUNT + '%',
                color: 'transparent'
            }
        );
    }
    return spots;
}

/**
 * Функция для получния JSX-компонентов мест парковки в первом (верхнем) ряду
 * @function getFirstParkingRow
 * @param {Array} content - данные о парковке из Store
 * @return {Array} spots - массив с JSX-компонентами парковочных мест
 * */
export function getSecondParkingRowCircles(content) {
    const circles = [];
    for (let index = 0; index < SECOND_PARKING_ROW_SPOTS_COUNT / 2; ++index) {
        const currentContent = content[FIRST_PARKING_ROW_SPOTS_COUNT + index];
        let color = 'green';
        if (currentContent.occupiedByVideo || currentContent.occupiedBySensors) {
            color = 'red';
        } else if (currentContent.reserved) {
            color = 'grey';
        }
        circles.push(
            {
                "x_axis": (50 / FIRST_PARKING_ROW_SPOTS_COUNT) + (index * 100 / FIRST_PARKING_ROW_SPOTS_COUNT) + '%',
                "y_axis": 75 + (25 / 2) - (CIRCLE_RADIUS / 2) + '%',
                "radius": CIRCLE_RADIUS + '%',
                color: color,
                id: FIRST_PARKING_ROW_SPOTS_COUNT + index
            }
        );
    }
    for (let index = SECOND_PARKING_ROW_SPOTS_COUNT / 2; index < SECOND_PARKING_ROW_SPOTS_COUNT; ++index) {
        const currentContent = content[FIRST_PARKING_ROW_SPOTS_COUNT + index];
        let color = 'green';
        if (currentContent.occupiedByVideo || currentContent.occupiedBySensors) {
            color = 'red';
        } else if (currentContent.reserved) {
            color = 'grey';
        }
        circles.push(
            {
                "x_axis": (50 / FIRST_PARKING_ROW_SPOTS_COUNT) + ((index + 2) * 100 / FIRST_PARKING_ROW_SPOTS_COUNT) + '%',
                "y_axis": 75 + (25 / 2) - (CIRCLE_RADIUS / 2) + '%',
                "radius": CIRCLE_RADIUS + '%',
                color: color,
                id: FIRST_PARKING_ROW_SPOTS_COUNT + index
            }
        );
    }
    return circles;
}

/**
 * Функция для получния JSX-компонентов мест парковки в первом (верхнем) ряду
 * @function getHorizontalLine
 * @return {Array} spots - массив с JSX-компонентами парковочных мест
 * */
export function getHorizontalLine() {
    return [
        {"x": 100 / FIRST_PARKING_ROW_SPOTS_COUNT + '%', "y": '50%'},
        {"x": 100 - (100 / FIRST_PARKING_ROW_SPOTS_COUNT) + "%", "y": '50%'}
    ];
}

/**
 * Функция для получния JSX-компонентов мест парковки в первом (верхнем) ряду
 * @function getVerticalLine
 * @return {Array} spots - массив с JSX-компонентами парковочных мест
 * */
export function getVerticalLine() {
    return [
        {"x": 100 / (SECOND_PARKING_ROW_SPOTS_COUNT / 2) + '%', "y": '100%'},
        {"x": 100 / (SECOND_PARKING_ROW_SPOTS_COUNT / 2) + '%', "y": '75%'}
    ];
}


/**
 * Функция для получния JSX-компонентов мест парковки в первом (верхнем) ряду
 * @function getFirstParkingRow
 * @param {Array} content - данные о парковке из Store
 * @return {Array} spots - массив с JSX-компонентами парковочных мест
 * */
export function getFirstParkingRowCircles(content) {
    const circles = [];
    for (let index = 0; index < FIRST_PARKING_ROW_SPOTS_COUNT; ++index) {
        const currentContent = content[index];
        let color = 'green';
        if (currentContent.occupiedByVideo || currentContent.occupiedBySensors) {
            color = 'red';
        } else if (currentContent.reserved) {
            color = 'grey';
        }
        circles.push(
            {
                "x_axis": (50 / FIRST_PARKING_ROW_SPOTS_COUNT) + (index * 100 / FIRST_PARKING_ROW_SPOTS_COUNT) + '%',
                "y_axis": (25 / 2) - (CIRCLE_RADIUS / 2) + '%',
                "radius": CIRCLE_RADIUS + '%',
                color: color,
                id: index
            }
        );
    }
    return circles;
}

/**
 * Функция для получения данных о стрелке до парковочного места
 * @function getArrowData
 * @param {Number} id - id выбранного места
 * @return {Array} polyline - массив с данными о стрелке
 * */
export function getArrowData(id) {
    if (isNaN(id)) {
        return null;
    }
    const polyline = [];
    const rowNumber = id - FIRST_PARKING_ROW_SPOTS_COUNT;
    if (rowNumber < 0) { //первый ряд
        polyline.push(
            {'x': (50 / FIRST_PARKING_ROW_SPOTS_COUNT) + 100 / (SECOND_PARKING_ROW_SPOTS_COUNT / 2), 'y': 100},
            {'x': (50 / FIRST_PARKING_ROW_SPOTS_COUNT) + 100 / (SECOND_PARKING_ROW_SPOTS_COUNT / 2), 'y': 62.5},
            {'x': 100 - (50 / FIRST_PARKING_ROW_SPOTS_COUNT), 'y': 62.5},
            {'x': 100 - (50 / FIRST_PARKING_ROW_SPOTS_COUNT), 'y': 37.5}
        );
        if (id === FIRST_PARKING_ROW_SPOTS_COUNT - 1) {
            /* Если выбрано последнее место в первом ряду - линия дальше прямая */
            polyline.push({'x': 100 - (50 / FIRST_PARKING_ROW_SPOTS_COUNT), 'y': 12.5});
        } else {
            /* Иначе - идем влево, затем вверх */
            polyline.push(
                {
                    'x': 100 - (((FIRST_PARKING_ROW_SPOTS_COUNT - id) * 100 - 50) / FIRST_PARKING_ROW_SPOTS_COUNT),
                    'y': 37.5
                },
                {
                    'x': 100 - (((FIRST_PARKING_ROW_SPOTS_COUNT - id) * 100 - 50) / FIRST_PARKING_ROW_SPOTS_COUNT),
                    'y': 12.5
                }
            );
        }
    } else { //второй ряд
        polyline.push(
            {'x': (50 / FIRST_PARKING_ROW_SPOTS_COUNT) + 100 / (SECOND_PARKING_ROW_SPOTS_COUNT / 2), 'y': 100},
            {'x': (50 / FIRST_PARKING_ROW_SPOTS_COUNT) + 100 / (SECOND_PARKING_ROW_SPOTS_COUNT / 2), 'y': 62.5}
        );
        if (id < (SECOND_PARKING_ROW_SPOTS_COUNT / 2) + FIRST_PARKING_ROW_SPOTS_COUNT) {
            /* Если выбрано место во втором ряду слева - надо рисовать стрелку с "разворотом" */
            polyline.push(
                {'x': 100 - (50 / FIRST_PARKING_ROW_SPOTS_COUNT), 'y': 62.5},
                {'x': 100 - (50 / FIRST_PARKING_ROW_SPOTS_COUNT), 'y': 37.5},
                {'x': 50 / FIRST_PARKING_ROW_SPOTS_COUNT, 'y': 37.5},
                {'x': 50 / FIRST_PARKING_ROW_SPOTS_COUNT, 'y': 62.5}
            );
            if (id === FIRST_PARKING_ROW_SPOTS_COUNT) {
                /* Если выбрано первое место во втором ряду - линия дальше прямая */
                polyline.push({'x': 50 / FIRST_PARKING_ROW_SPOTS_COUNT, 'y': 87.5});
            } else {
                /* Иначе - идем вправо, затем вниз */
                polyline.push(
                    {
                        'x': 100 - (((PARKING_SPOTS_COUNT - id + 2) * 100 - 50) / FIRST_PARKING_ROW_SPOTS_COUNT),
                        'y': 62.5
                    },
                    {
                        'x': 100 - (((PARKING_SPOTS_COUNT - id + 2) * 100 - 50) / FIRST_PARKING_ROW_SPOTS_COUNT),
                        'y': 87.5
                    }
                );
            }
        } else {
            /* Иначе - вправо, затем вниз */
            polyline.push(
                {
                    'x': 100 - (((PARKING_SPOTS_COUNT - id) * 100 - 50) / FIRST_PARKING_ROW_SPOTS_COUNT),
                    'y': 62.5
                },
                {
                    'x': 100 - (((PARKING_SPOTS_COUNT - id) * 100 - 50) / FIRST_PARKING_ROW_SPOTS_COUNT),
                    'y': 87.5
                }
            );
        }
    }
    return polyline;
}

// /**
//  * Функция для получния JSX-компонентов мест парковки во втором (нижнем) ряду
//  * @function getSecondParkingRow
//  * @param {Array} content - данные о парковке из Store
//  * @param {Function} onClick - обработчик клика на парковочное место
//  * @return {Array} spots - массив с JSX-компонентами парковочных мест
//  * */
// export function getSecondParkingRow(content, onClick) {
//     const spots = [];
//     let noBorder = false, leftBorder = false;
//     for (let index = 0; index < SECOND_PARKING_ROW_SPOTS_COUNT; ++index) {
//         const currentContent = content[FIRST_PARKING_ROW_SPOTS_COUNT + index];
//         if (index === SECOND_PARKING_ROW_SPOTS_COUNT - 1) { noBorder = true; }
//         if (index === SECOND_PARKING_ROW_SPOTS_COUNT - 2) { leftBorder = true; }
//         spots.push(
//             <ParkingSpot
//                 key={index}
//                 leftBorder={leftBorder}
//                 noBorder={noBorder}
//                 onSpotClick={onClick.bind(null, currentContent ? currentContent.id : null)}
//                 occupied={currentContent ? currentContent.reserved || currentContent.occupied : null}
//                 reserved={currentContent ? currentContent.reserved : null}
//                 text={currentContent ? currentContent.name : null}
//             />
//         );
//         if (index === 1) {
//             spots.push(<RoadCell key={`cell${index}`} />);
//             spots.push(<RoadCell key={`cell${index + 1}`} noBorder={true} />);
//         }
//     }
//     return spots;
// }