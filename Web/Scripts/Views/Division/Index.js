$(window).load(updateTree);

function updateTree() {
    $("#tree").treegrid();
    $("#tree").treegrid("collapseAll");
    spinner.stop();
}