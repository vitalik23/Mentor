let popup = document.getElementById('mypopup'),
    popupToggle = document.getElementById('myBtn'),
    popupClose = document.querySelector('.close');

popupToggle.onclick = function () {
    popup.style.display = "block";
}

popupClose.onclick = function () {
    popup.style.display = "none";
}

window.onclick = function (e) {
    if (e.target == popup) {
        popup.style.display = "none";
    }
}