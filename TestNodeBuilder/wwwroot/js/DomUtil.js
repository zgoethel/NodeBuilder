export function getCenterCoords(selector) {
    const elem = document.querySelector(selector);
    const box = elem.getBoundingClientRect();

    const centerX = ((box.left + box.right) / 2).toFixed(2);
    const centerY = ((box.top + box.bottom) / 2).toFixed(2);

    return `${centerX},${centerY}`;
}