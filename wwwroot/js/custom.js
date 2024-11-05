function sortTable(columnIndex, tableId) {
    const table = document.getElementById(tableId);
    const tbody = table.querySelector('tbody');
    const rows = Array.from(tbody.querySelectorAll('tr'));

    const isAscending = table.getAttribute('data-sort-order') === 'asc';
    const direction = isAscending ? 1 : -1;

    const isNumeric = !isNaN(rows[0].cells[columnIndex].textContent.trim());

    rows.sort((rowA, rowB) => {
        const cellA = rowA.cells[columnIndex].textContent.trim();
        const cellB = rowB.cells[columnIndex].textContent.trim();

        // Try to parse as dates in 'yyyy-mm-dd' format
        const dateA = parseDate(cellA);
        const dateB = parseDate(cellB);

        if (dateA && dateB) {
            return (dateA - dateB) * direction;
        } else if (isNumeric) {
            return (parseFloat(cellA) - parseFloat(cellB)) * direction;
        } else {
            const strA = cellA.toLowerCase();
            const strB = cellB.toLowerCase();

            if (strA < strB) {
                return -1 * direction;
            }
            if (strA > strB) {
                return 1 * direction;
            }
            return 0;
        }
    });

    // Reorder rows in the table based on the sorted rows array
    rows.forEach(row => tbody.appendChild(row));

    // Toggle sort order attribute
    table.setAttribute('data-sort-order', isAscending ? 'desc' : 'asc');
}

// Helper function to parse dates in 'yyyy-mm-dd' format
function parseDate(dateString) {
    const parsedDate = Date.parse(dateString);
    return isNaN(parsedDate) ? null : parsedDate;
}


function searchTable(sectionId, searchId) {
    const input = document.getElementById(searchId);
    const filter = input.value.toLowerCase();
    const table = document.querySelector(`#${sectionId} table`);
    const rows = table.getElementsByTagName("tr");

    // Loop through all rows (starting from 1 to skip the header row)
    for (let i = 1; i < rows.length; i++) {
        const cells = rows[i].getElementsByTagName("td");
        let rowVisible = false;

        // Loop through all cells in the row
        for (let j = 0; j < cells.length; j++) {
            if (cells[j].textContent.toLowerCase().includes(filter)) {
                rowVisible = true;
                break;
            }
        }

        // Show or hide the row based on whether it matches the filter
        rows[i].style.display = rowVisible ? "" : "none";
    }
}
