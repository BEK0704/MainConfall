/// <reference path="audiorecord.js" />
var atime = "Feb 18, 2021 18:35:25";
var qtime = "Feb 18, 2021 18:35:25";
function myFunction() {
    if (!location.hash) {
        location.hash = Math.floor(Math.random() * 0xFFFFFF).toString(16);
    }

    const roomHash = location.hash.substring(1);


        $.ajax({
            type: 'POST',
            contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
            data: { parameter1: roomHash },
            url: "/Home/Room1",
            success: function (data) {
                if (data.lets == true) {
                    self.location.replace('/Home/Room#' + data.positions);
                } else {
                    $("#Modal").modal();
                    $("#indexmodal").empty();
                    $("#indexmodal").append(data.positions);

                }
            }
        });
}
function SecondRoom1() {
    


    $.ajax({
        type: 'POST',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        url: "/Home/SecondRoom1",
        success: function (data) {
            if (data.lets == true) {
                self.location.replace('/Home/SecondRoom');
            } else {
                $("#Modal").modal();
                $("#indexmodal").empty();
                $("#indexmodal").append(data.positions);

            }
        }
    });
}
function ThirdRoom1() {

    $.ajax({
        type: 'POST',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        url: "/Home/ThirdRoom1",
        success: function (data) {
            if (data.lets == true) {
                self.location.replace('/Home/ThirdRoom');
            } else {
                $("#Modal").modal();
                $("#indexmodal").empty();
                $("#indexmodal").append(data.positions);

            }
        }
    });
}
function Photoinsert() {
    var resultb64 = "";
    var canvas = document.getElementById('canvas');
    var video = document.getElementById('remoteVideo');
    const context = canvas.getContext('2d');
    context.clearRect(0, 0, canvas.width, canvas.height);
    canvas.width = 320;
    canvas.height =240;
    canvas.getContext('2d').drawImage(video, 0, 0, 320, 240);
    resultb64 = canvas.toDataURL();
    $.ajax({
        type: 'POST',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: { parameter1: resultb64 },
        url: "/Home/Photoinsert",
        success: function (data) {
            if (data.lets == true)
                console.log("Rasm bazaga yuklandi"); else {
                console.log("Rasm bazaga yuklanmadi"); 
            }
        }
    });
}
function myFunction2() {
    
    var qlev= $('#questionlevel');
    var qbody = $('#questionbody');
    var timer1 = document.getElementById('demo');
    var timer2 = document.getElementById('time2');
    // capture


    $.ajax({
        type: 'Get',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
     
        url: "/Home/Start",
        success: function (data) {
            if (data.part == 1) {
                document.getElementById("startbutton").disabled = true;
                timer1.style.visibility = "visible";
                timer2.style.visibility = "visible";
                document.getElementById("nextbutton").disabled = false;
                qlev.empty();
                qlev.append(data.level);
                qbody.empty();
                qbody.append(data.question)
                countDownDate1 = new Date(data.alltime).getTime();
                countDownDate = new Date(data.qtime).getTime();
                timenow = new Date(data.ntime).getTime();
            }
            
        }
    });
    Photoinsert();
    setTimeout(function () { startrecoding(); }, 2000);
}

function bigImg() {
    document.getElementById("chetlatish").src = "/images/icons/51.png";
}

function normalImg() {
    document.getElementById("chetlatish").src = "/images/icons/52.png";
}



function earnvalues() {
    var a1 = 0, a2 = 0, a3 = 0, a4 = 0, a5 = 0;
    var radios = document.getElementsByName('inlineRadioOptions');
    $('#wordsmark').empty();
    for (var i = 0, length = radios.length; i < length; i++) {
        if (radios[i].checked) {
            // do whatever you want with the checked radio
            $('#wordsmark').append(radios[i].value);
            a1 = radios[i].value;
            // only one radio can be logically checked, don't check the rest
            break;
        }
    }
    radios = document.getElementsByName('inlineRadioOptions1');
    $('#grammark').empty();
    for (var i = 0, length = radios.length; i < length; i++) {
        if (radios[i].checked) {
            // do whatever you want with the checked radio
            $('#grammark').append(radios[i].value);
            a2 = radios[i].value;
            // only one radio can be logically checked, don't check the rest
            break;
        }
    }
    radios = document.getElementsByName('inlineRadioOptions2');
    $('#spechmark').empty();
    for (var i = 0, length = radios.length; i < length; i++) {
        if (radios[i].checked) {
            // do whatever you want with the checked radio
            $('#spechmark').append(radios[i].value);
            a3 = radios[i].value;
            // only one radio can be logically checked, don't check the rest
            break;
        }
    }
    radios = document.getElementsByName('inlineRadioOptions3');
    $('#comumark').empty();
    for (var i = 0, length = radios.length; i < length; i++) {
        if (radios[i].checked) {
            // do whatever you want with the checked radio
            $('#comumark').append( radios[i].value);
            a4 = radios[i].value;
            // only one radio can be logically checked, don't check the rest
            break;
        }
    }
    radios = document.getElementsByName('inlineRadioOptions4');
    $('#promark').empty();
    for (var i = 0, length = radios.length; i < length; i++) {
        if (radios[i].checked) {
            // do whatever you want with the checked radio
            $('#promark').append(radios[i].value);
            a5 = radios[i].value;
            // only one radio can be logically checked, don't check the rest
            break;
        }
    }
    document.getElementById("finishtext").value = document.getElementById("accortext").value;
    $('#allmarks').empty();
    var a6 = Number(a1) + Number(a2) + Number(a3) + Number(a4) + Number(a5);
    $('#allmarks').append('<b>'+a6+'</b>');
}
function thirdmark() {
    var words, gramm, speach, comunic, pronun;
    var radios = document.getElementsByName('inlineRadioOptions');
    for (var i = 0, length = radios.length; i < length; i++) {
        if (radios[i].checked) {
            // do whatever you want with the checked radio
            words=radios[i].value;

            // only one radio can be logically checked, don't check the rest
            break;
        }
    }
    radios = document.getElementsByName('inlineRadioOptions1');
    for (var i = 0, length = radios.length; i < length; i++) {
        if (radios[i].checked) {
            // do whatever you want with the checked radio
           gramm=radios[i].value;

            // only one radio can be logically checked, don't check the rest
            break;
        }
    }
    radios = document.getElementsByName('inlineRadioOptions2');
    for (var i = 0, length = radios.length; i < length; i++) {
        if (radios[i].checked) {
            // do whatever you want with the checked radio
            speach=radios[i].value;

            // only one radio can be logically checked, don't check the rest
            break;
        }
    }
    radios = document.getElementsByName('inlineRadioOptions3');
    for (var i = 0, length = radios.length; i < length; i++) {
        if (radios[i].checked) {
            // do whatever you want with the checked radio
            comunic=radios[i].value;

            // only one radio can be logically checked, don't check the rest
            break;
        }
    }
    radios = document.getElementsByName('inlineRadioOptions4');
    for (var i = 0, length = radios.length; i < length; i++) {
        if (radios[i].checked) {
            // do whatever you want with the checked radio
            pronun=radios[i].value;

            // only one radio can be logically checked, don't check the rest
            break;
        }
    }
    $.ajax({
        type: 'POST',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: { parameter1: words, parameter2: gramm, parameter3: speach, parameter4: comunic, parameter5: pronun, parameter6: document.getElementById("finishtext").value },
        url: "/Home/Thirdmark",

        success: function (data) {
            if (data.lets == true) {
                self.location.replace('/Home/Index');
            } else {
                alert('Baholashda xatolik!');

            }
        }
    });
}

function myfunction5() {
   // document.getElementById("marked").disabled = true;
    var words, gramm, speach, comunic, pronun;
    var radios = document.getElementsByName('inlineRadioOptions');
    for (var i = 0, length = radios.length; i < length; i++) {
        if (radios[i].checked) {
            // do whatever you want with the checked radio
            words = radios[i].value;

            // only one radio can be logically checked, don't check the rest
            break;
        }
    }
    radios = document.getElementsByName('inlineRadioOptions1');
    for (var i = 0, length = radios.length; i < length; i++) {
        if (radios[i].checked) {
            // do whatever you want with the checked radio
            gramm = radios[i].value;

            // only one radio can be logically checked, don't check the rest
            break;
        }
    }
    radios = document.getElementsByName('inlineRadioOptions2');
    for (var i = 0, length = radios.length; i < length; i++) {
        if (radios[i].checked) {
            // do whatever you want with the checked radio
            speach = radios[i].value;

            // only one radio can be logically checked, don't check the rest
            break;
        }
    }
    radios = document.getElementsByName('inlineRadioOptions3');
    for (var i = 0, length = radios.length; i < length; i++) {
        if (radios[i].checked) {
            // do whatever you want with the checked radio
            comunic = radios[i].value;

            // only one radio can be logically checked, don't check the rest
            break;
        }
    }
    radios = document.getElementsByName('inlineRadioOptions4');
    for (var i = 0, length = radios.length; i < length; i++) {
        if (radios[i].checked) {
            // do whatever you want with the checked radio
            pronun = radios[i].value;

            // only one radio can be logically checked, don't check the rest
            break;
        }
    }
    console.log("Palakat ishladi");
    $.ajax({
        type: 'POST',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: { parameter1: words, parameter2: gramm, parameter3: speach, parameter4: comunic, parameter5: pronun, parameter6: document.getElementById("finishtext").value},
        url: "/Home/Finish",

        success: function (data) {
            if (data.lets == true) {
                $("#staticBackdrop").modal("hide");
                checkaudiocand1();
            } else {
                $("#staticBackdrop").modal("hide");
                checkaudiocand1();

            }
        }
    });
    checkaudiocand1();
}
function myfunction6() {
    $.ajax({
        type: 'POST',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: { parameter6: document.getElementById("chetlatishtext").value },
        url: "/Home/Exclude",

        success: function (data) {
            if (data.lets == true) {
                self.location.replace('/Home/Index');
            } else {
                alert('Chetlatishda xatolik!');

            }
        }
    });
}

function myfunction7() {
    $.ajax({
        type: 'POST',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: { parameter6: document.getElementById("nosoz").value },
        url: "/Home/Bugs",

        success: function (data) {
            if (data.lets == true) {
                self.location.replace('/Home/Index');
            } else {
                alert('Nosozlikni tasdiqlashda xatolik!');

            }
        }
    });
}
function seconmark() {
    var words, gramm, speach, comunic, pronun;
    var radios = document.getElementsByName('inlineRadioOptions');
    for (var i = 0, length = radios.length; i < length; i++) {
        if (radios[i].checked) {
            // do whatever you want with the checked radio
            words = radios[i].value;

            // only one radio can be logically checked, don't check the rest
            break;
        }
    }
    radios = document.getElementsByName('inlineRadioOptions1');
    for (var i = 0, length = radios.length; i < length; i++) {
        if (radios[i].checked) {
            // do whatever you want with the checked radio
            gramm = radios[i].value;

            // only one radio can be logically checked, don't check the rest
            break;
        }
    }
    radios = document.getElementsByName('inlineRadioOptions2');
    for (var i = 0, length = radios.length; i < length; i++) {
        if (radios[i].checked) {
            // do whatever you want with the checked radio
            speach = radios[i].value;

            // only one radio can be logically checked, don't check the rest
            break;
        }
    }
    radios = document.getElementsByName('inlineRadioOptions3');
    for (var i = 0, length = radios.length; i < length; i++) {
        if (radios[i].checked) {
            // do whatever you want with the checked radio
            comunic = radios[i].value;

            // only one radio can be logically checked, don't check the rest
            break;
        }
    }
    radios = document.getElementsByName('inlineRadioOptions4');
    for (var i = 0, length = radios.length; i < length; i++) {
        if (radios[i].checked) {
            // do whatever you want with the checked radio
            pronun = radios[i].value;

            // only one radio can be logically checked, don't check the rest
            break;
        }
    }
    $.ajax({
        type: 'POST',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: { parameter1: words, parameter2: gramm, parameter3: speach, parameter4: comunic, parameter5: pronun, parameter6: document.getElementById("finishtext").value },
        url: "/Home/Secondmark",

        success: function (data) {
            if (data.lets == true) {
                self.location.replace('/Home/Index');
            } else {
                alert('Baholashda xatolik!');

            }
        }
    });
}