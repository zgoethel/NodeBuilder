export function getCenterCoords(selector, relativeTo) {
    const origin = { x: 0, y: 0 };
    if (relativeTo) {
        const _origin = getCenterCoords(relativeTo, null);

        origin.x = +_origin.split(",")[0];
        origin.y = +_origin.split(",")[1];
    }

    const elem = document.querySelector(selector);
    const box = elem.getBoundingClientRect();

    const centerX = ((box.left + box.right) / 2 - origin.x).toFixed(2);
    const centerY = ((box.top + box.bottom) / 2 - origin.y).toFixed(2);

    return `${centerX},${centerY}`;
}