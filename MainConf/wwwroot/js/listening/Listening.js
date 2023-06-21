function Enterlistening() {
    if (!location.hash) {
        location.hash = Math.floor(Math.random() * 0xFFFFFF).toString(16);
    }

    const roomHash = location.hash.substring(1);


    $.ajax({
        type: 'POST',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: { parameter1: roomHash, parameter2: document.getElementById('voluem').value },
        url: "/Listening/Room",
        success: function (data) {
            if (data.lets == true) {
                Photoinsert1(data.part, data.positions);
                
            } else {
                $("#Modal123").modal();
                $("#indexmodal123").empty();
                $("#indexmodal123").append(data.positions);

            }
        }
    });
}

function Photoinsert1(a,b) {
    var resultb641 = "";
    var canvas = document.getElementById('canvas');
    var video = document.getElementById('video123');
    const context = canvas.getContext('2d');
    context.clearRect(0, 0, canvas.width, canvas.height);
    canvas.width = 320;
    canvas.height = 240;
    canvas.getContext('2d').drawImage(video, 0, 0, 320, 240);
    resultb641 = canvas.toDataURL();
    $.ajax({
        type: 'POST',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: { parameter1: resultb641, parameter2: a },
        url: "/Listening/Photoinsert",
        success: function (data) {
            if (data.lets == true)
            {
                //Photoinsert1();
                if (a == 1) {self.location.replace('/Listening/Exampart1');}
                else if (a == 2) { self.location.replace('/Listening/Exampart2'); }
                else if (a == 3) { self.location.replace('/Listening/Exampart3'); }
                else if (a == 4) { self.location.replace('/Listening/Exampart4'); }
                else if (a == 5) { self.location.replace('/Listening/Exampart5'); }
                else if (a == 6) { self.location.replace('/Listening/Exampart6'); }
                else {self.location.replace('/Home/Privacy');}
                
            } else {
                console.log("Rasm bazaga yuklanmadi");
            }
        }
    });
}
function Contreading(a) {
    var resultb641 = "";
    var canvas = document.getElementById('canvas');
    var video = document.getElementById('local-video');
    const context = canvas.getContext('2d');
    context.clearRect(0, 0, canvas.width, canvas.height);
    canvas.width = 320;
    canvas.height = 240;
    canvas.getContext('2d').drawImage(video, 0, 0, 320, 240);
    resultb641 = canvas.toDataURL();
    breakingexaml(a, resultb641);

}

function breakingexaml(a, b) {
    //  console.log(a);
    $.ajax({
        type: 'POST',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: { type: a, photo: b },
        url: "/Listening/Posterror",
        success: function (data) {
            if (data.lets == true) {

            } else {
                console.log(data.positions);
            }
        }
    });
}
function Contreadingr(a) {
    var resultb641 = "";
    var canvas = document.getElementById('canvas');
    var video = document.getElementById('local-video');
    const context = canvas.getContext('2d');
    context.clearRect(0, 0, canvas.width, canvas.height);
    canvas.width = 320;
    canvas.height = 240;
    canvas.getContext('2d').drawImage(video, 0, 0, 320, 240);
    resultb641 = canvas.toDataURL();
    breakingexamr(a, resultb641);

}
function breakingexamr(a, b) {
    //  console.log(a);
    $.ajax({
        type: 'POST',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: { type: a, photo: b },
        url: "/Reading/Posterror",
        success: function (data) {
            if (data.lets == true) {

            } else {
                console.log(data.positions);
            }
        }
    });
}