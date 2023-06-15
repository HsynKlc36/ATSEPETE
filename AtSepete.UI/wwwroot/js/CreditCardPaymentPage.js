function getInputValue(textId, changedtextId) {
    var degistiren = document.getElementById(textId);
    var degisen = document.getElementById(changedtextId);
    degisen.value = degistiren.value;
    degisen.style.border = "3px solid white";
    degisen.style.borderRadius = "3px";
    degisen.style.transition = "0.5s";
}
function getInputValueCvv(textId, changedtextId) {
    var degistiren = document.getElementById(textId);
    var degisen = document.getElementById(changedtextId);
    degisen.value = degistiren.value;
    degisen.style.transition = "0.5s";
}
function getInputValueandText(textId, changedtextId) {
    var degistiren = document.getElementById(textId);
    var degisen = document.getElementById(changedtextId);
    degisen.innerHTML = degistiren.value;
    degisen.style.border = "3px solid white";
    degisen.style.borderRadius = "3px";
    degisen.style.transition = "0.5s";
}
function getText(textId, changedtextId) {
    var degistiren = document.getElementById(textId);
    var degisen = document.getElementById(changedtextId);
    degisen.innerHTML = degistiren.options[degistiren.selectedIndex].text;
    degisen.style.fontSize = "19px";
    degisen.style.border = "3px solid white";
    degisen.style.borderRadius = "3px";
    degisen.style.transition = "0.5s";
}
function borderRemove(id) {
    var column = document.getElementById(id);
    column.style.border = "none";
}
function reverse(class1, class2) {
    var front = document.getElementById(class1);
    var back = document.getElementById(class2);
    front.style.transform = "rotateY(180deg)";
    front.style.opacity = "0";
    back.style.transform = "rotateY(360deg)";
    back.style.opacity = "1";
}
function reverseback(class1, class2) {
    var front = document.getElementById(class1);
    var back = document.getElementById(class2);
    front.style.transform = "rotateY(0deg)";
    front.style.opacity = "1";
    back.style.transform = "rotateY(180deg)";
    back.style.opacity = "0";
}



function modify(degistirenId, degisenId) {
    var degistiren = document.getElementById(degistirenId);
    var leng = String(degistiren.value).length;
    console.log(leng.toString());
    var degisen = document.getElementById(degisenId);
    var children = degisen.children;

    if (leng < 5) {
        children[0].innerHTML = degistiren.value;
        children[1].innerHTML = "####";
        children[2].innerHTML = "####";
        children[3].innerHTML = "####";
    } else if (leng < 9) {
        var old = children[1].innerHTML == "####" ? "" : children[1].innerHTML;
        children[1].innerHTML = old + "*";
        children[2].innerHTML = "####";
        children[3].innerHTML = "####";
    } else if (leng < 13) {
        var old = children[2].innerHTML == "####" ? "" : children[2].innerHTML;
        children[2].innerHTML = old + "*";
        children[3].innerHTML = "####";
    } else if (leng < 17) {
        children[3].innerHTML = String(degistiren.value).substring(12, 16);
    } else {
        console.error("karakter uzunluğu hatası");
    }

    if (leng === 0) {
        children[0].innerHTML = "####";
        children[1].innerHTML = "####";
        children[2].innerHTML = "####";
        children[3].innerHTML = "####";
    }
}


