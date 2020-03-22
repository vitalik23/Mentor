
function doIt(selector, baseUrl) {

    var groupId      = document.getElementById("groupId");
    var departmentId = document.getElementById("departmentId");
    var facultyId    = document.getElementById("facultyId");

    var nameId    = document.getElementById("nameId");
    var surnameId = document.getElementById("surnameId");

    var strGr  = groupId.options[groupId.selectedIndex].value;
    var strDep = departmentId.options[departmentId.selectedIndex].value;
    var strFac = facultyId.options[facultyId.selectedIndex].value;

    window.location.href =
        baseUrl +
        '?name=' + nameId.value + 
        '&surname=' + surnameId.value + 
        '&facultyId=' + strFac +
        '&departmentId=' + strDep +
        '&groupId=' + strGr +
        '&selector=' + selector;
}