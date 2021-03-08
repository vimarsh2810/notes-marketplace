// ====== Pagination ==========

function Pager(tableName, itemsPerPage) {
    "use strict";

    this.tableName = tableName;
    this.itemsPerPage = itemsPerPage;
    this.currentPage = 1;
    this.pages = 0;
    this.inited = false;

    this.showRecords = function (from, to) {
        let rows = document.getElementById(tableName).rows;

        for (let i = 1; i < rows.length; i++) {
            if (i < from || i > to) {
                rows[i].style.display = "none";
            } else {
                rows[i].style.display = "";
            }
        }
    };

    this.showPage = function (pageNumber) {
        if (!this.inited) {
            return;
        }

        let oldPageAnchor = document.getElementById("pg" + this.currentPage);
        oldPageAnchor.className = "pg-normal";

        this.currentPage = pageNumber;
        let newPageAnchor = document.getElementById("pg" + this.currentPage);
        newPageAnchor.className = "pg-selected";

        let from = (pageNumber - 1) * itemsPerPage + 1;
        let to = from + itemsPerPage - 1;
        this.showRecords(from, to);

        let pgNext = document.querySelector(".pg-next"),
            pgPrev = document.querySelector(".pg-prev");

    };

    this.prev = function () {
        if (this.currentPage > 1) {
            this.showPage(this.currentPage - 1);
        }
    };

    this.next = function () {
        if (this.currentPage < this.pages) {
            this.showPage(this.currentPage + 1);
        }
    };

    this.init = function () {
        let rows = document.getElementById(tableName).rows;
        let records = rows.length - 1;

        this.pages = Math.ceil(records / itemsPerPage);
        this.inited = true;
    };

    this.showPageNav = function (pagerName, positionId) {
        if (!this.inited) {
            return;
        }

        let element = document.getElementById(positionId),
            pagerHtml =
                '<span onclick="' +
                pagerName +
                '.prev();" class="pg-normal"><img src="../images/left-arrow.png"></span>';

        for (let page = 1; page <= this.pages; page++) {
            pagerHtml +=
                '<span id="pg' +
                page +
                '" class="pg-normal pg-next" onclick="' +
                pagerName +
                ".showPage(" +
                page +
                ');">' +
                page +
                "</span>";
        }

        pagerHtml +=
            '<span onclick="' +
            pagerName +
            '.next();" class="pg-normal"><img src="../images/right-arrow.png"></span>';

        element.innerHTML = pagerHtml;
    };
}

let pager = new Pager("pager", 5);

pager.init();
pager.showPageNav("pager", "pageNavPosition");
pager.showPage(1);