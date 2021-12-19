function CloseApp() {
    
   
    window.open('', '_parent', '');
    window.close();
}
function SetWaitCursor() {
    document.body.style.cursor = 'wait';
}
function SetDefCursor() {
    document.body.style.cursor = 'default';
}

function DisplayWaitMsg(show) {
    var display;
    if(show==true)
        display="block";
    else
        display="none";
    //document.getElementById("WaitMsg").style.display = display;
}

function PrintRecsCount(recs) {
    //return;
    var ctrl=document.getElementById("recs_count");
    if (recs > 0)
        ctrl.innerText ="כמות רשומות:"+recs;
    else
        ctrl.innerText = "";
}
function toggleVisibility(isDisplayed) {
    //var control = document.getElementById("WaitMsg");
    //var clsname;
    //if (isDisplayed == true)
    //{
    //    clsname = "loader";
    //    control.style.display="block";
    //}
        
    //else
    //{
    //    clsname = "imHidden";
    //    control.style.display = "none";
    //}
        

    //control.className = clsname;
    //control.Style.Add("display", "none;");
    //if (control.style.visibility == "visible" || control.style.visibility == "")
    //    control.style.visibility = "hidden";
    //else
    //    control.style.visibility = "visible";
    //if (isDisplayed == true) {
    //    control.style.visibility = "visible";
    //    //window.location.reload();
    //    //location.reload(true);
    //}
    //else {
    //    control.style.visibility = "hidden";
    //}
    

    //if (isDisplayed == true) {
    //    control.style.visibility = "visible";
    //    //window.location.reload();
    //    //location.reload(true);
    //}
    //else {
    //    control.style.visibility = "hidden";
    //}

}

//$(document).ready(function () {

//    $("#main").hover(function () {
//        //workws!!!!also alert("hhh");
//    });

//});
