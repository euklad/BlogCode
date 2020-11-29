function elementsLine(svgContainer, svgElem, lineElem, startElem, endElem) {
    var svgContainerElem = $(svgContainer);

    // get (top, left) corner coordinates of the svg container   
    var svgTop = svgContainerElem.offset().top;
    var svgLeft = svgContainerElem.offset().left;

    // if first element is lower than the second, swap!
    if (startElem.offset().top > endElem.offset().top) {
        var temp = startElem;
        startElem = endElem;
        endElem = temp;
    }


    // get (top, left) coordinates for the two elements
    var startCoord = startElem.offset();
    var endCoord = endElem.offset();

    // calculate path's start (x,y)  coords
    // we want the x coordinate to visually result in the element's mid point
    var startX = startCoord.left + 0.5 * startElem.width() - svgLeft;    // x = left offset + 0.5*width - svg's left offset
    var startY = startCoord.top + 0.5 * startElem.height() - svgTop;        // y = top offset + height - svg's top offset

    // calculate path's end (x,y) coords
    var endX = endCoord.left + 0.5 * endElem.width() - svgLeft;
    var endY = endCoord.top + 0.5 * endElem.height() - svgTop;

    // call function for drawing the path
    // drawPath(svg, path, startX, startY, endX, endY);

    var line = lineElem;
    var stroke = parseFloat(line.attr("stroke-width"));

    // check if the svg is big enough to draw the path, if not, set heigh/width
    if (svgElem.attr("height") < endY) svgElem.attr("height", endY + stroke);
    if (svgElem.attr("height") < startY) svgElem.attr("height", startY + stroke);
    if (svgElem.attr("width") < (startX + stroke)) svgElem.attr("width", (startX + stroke));
    if (svgElem.attr("width") < (endX + stroke)) svgElem.attr("width", (endX + stroke));

    line.attr("x1", startX);
    line.attr("y1", startY);
    line.attr("x2", endX);
    line.attr("y2", endY);
}


//helper functions, it turned out chrome doesn't support Math.sgn()
function signum(x) {
    return (x < 0) ? -1 : 1;
}
function absolute(x) {
    return (x < 0) ? -x : x;
}

function drawPath(svg, path, startX, startY, endX, endY) {
    // get the path's stroke width (if one wanted to be  really precize, one could use half the stroke size)
    var stroke = parseFloat(path.attr("stroke-width"));
    // check if the svg is big enough to draw the path, if not, set heigh/width
    if (svg.attr("height") < endY) svg.attr("height", endY);
    if (svg.attr("width") < (startX + stroke)) svg.attr("width", (startX + stroke));
    if (svg.attr("width") < (endX + stroke)) svg.attr("width", (endX + stroke));

    var deltaX = (endX - startX) * 0.15;
    var deltaY = (endY - startY) * 0.15;
    // for further calculations which ever is the shortest distance
    var delta = deltaY < absolute(deltaX) ? deltaY : absolute(deltaX);

    // set sweep-flag (counter/clock-wise)
    // if start element is closer to the left edge,
    // draw the first arc counter-clockwise, and the second one clock-wise
    var arc1 = 0; var arc2 = 1;
    if (startX > endX) {
        arc1 = 1;
        arc2 = 0;
    }
    // draw tha pipe-like path
    // 1. move a bit down, 2. arch,  3. move a bit to the right, 4.arch, 5. move down to the end 
    path.attr("d", "M" + startX + " " + startY +
        " V" + (startY + delta) +
        " A" + delta + " " + delta + " 0 0 " + arc1 + " " + (startX + delta * signum(deltaX)) + " " + (startY + 2 * delta) +
        " H" + (endX - delta * signum(deltaX)) +
        " A" + delta + " " + delta + " 0 0 " + arc2 + " " + endX + " " + (startY + 3 * delta) +
        " V" + endY);
}

function connectElements(svg, path, startElem, endElem) {
    var svgContainer = $("#svgContainer");

    // if first element is lower than the second, swap!
    if (startElem.offset().top > endElem.offset().top) {
        var temp = startElem;
        startElem = endElem;
        endElem = temp;
    }

    // get (top, left) corner coordinates of the svg container   
    var svgTop = svgContainer.offset().top;
    var svgLeft = svgContainer.offset().left;

    // get (top, left) coordinates for the two elements
    var startCoord = startElem.offset();
    var endCoord = endElem.offset();

    // calculate path's start (x,y)  coords
    // we want the x coordinate to visually result in the element's mid point
    var startX = startCoord.left + 0.5 * startElem.width() - svgLeft;    // x = left offset + 0.5*width - svg's left offset
    var startY = startCoord.top + 0.5 * startElem.height() - svgTop;        // y = top offset + height - svg's top offset

    // calculate path's end (x,y) coords
    var endX = endCoord.left + 0.5 * endElem.width() - svgLeft;
    var endY = endCoord.top + 0.5 * endElem.height() - svgTop;

    // call function for drawing the path
    // drawPath(svg, path, startX, startY, endX, endY);

    var line = $("#line1");
    var stroke = parseFloat(line.attr("stroke-width"));

    // check if the svg is big enough to draw the path, if not, set heigh/width
    if (svg.attr("height") < endY) svg.attr("height", endY + stroke);
    if (svg.attr("height") < startY) svg.attr("height", startY + stroke);
    if (svg.attr("width") < (startX + stroke)) svg.attr("width", (startX + stroke));
    if (svg.attr("width") < (endX + stroke)) svg.attr("width", (endX + stroke));

    line.attr("x1", startX);
    line.attr("y1", startY);
    line.attr("x2", endX);
    line.attr("y2", endY);
}



function connectAll() {
    // connect all the paths you want!
    connectElements($("#svg1"), $("#path1"), $("#teal"), $("#orange"));
    connectElements($("#svg1"), $("#path2"), $("#red"), $("#orange"));
    connectElements($("#svg1"), $("#path3"), $("#teal"), $("#aqua"));
    connectElements($("#svg1"), $("#path4"), $("#red"), $("#aqua"));
    connectElements($("#svg1"), $("#path5"), $("#purple"), $("#teal"));
    connectElements($("#svg1"), $("#path6"), $("#orange"), $("#green"));

}

function refreshAll() {
    $("#svg1").attr("height", "0");
    $("#svg1").attr("width", "0");
    // connectAll();
    connectElements($("#svg1"), $("#path1"), $("#t1"), $("#t2"));
    connectElements($("#svg1"), $("#path2"), $("#t1"), $("#t2"));
}

$(document).ready(function () {
    // reset svg each time 
    //$("#svg1").attr("height", "0");
    //$("#svg1").attr("width", "0");
    //connectAll();
});

$(window).resize(function () {
    // reset svg each time 
    //$("#svg1").attr("height", "0");
    //$("#svg1").attr("width", "0");
    //connectAll();
});