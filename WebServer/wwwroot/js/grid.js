// window.addEventListener("resize", setupGrid);
window.addEventListener("DOMContentLoaded", setupGrid);

function setupGrid() {
    const cellSize = 200; // Example of cell size in pixels
    const container = document.getElementById("container")
    let columnCount = Math.floor(container.offsetWidth / cellSize);
    columnCount = columnCount - (columnCount % 3);
    container.style.setProperty('--column-count', columnCount);

    const grid = document.getElementById("grid")
    new Sortable(grid, {
        // swap: true,
        // swapClass: '.highlight',
        filter: ".not-draggable",
        group: "grids",
        animation: 150,

        onMove: function (evt, originalEvent) {
            // Don't swap if not-draggable tile
            if (evt.related.classList.contains("not-draggable")) {
                return false;
            }
        },

        onEnd: function (evt) {
            console.log(`Swapped around tile ${evt.item.id}`)
        }
    });
}