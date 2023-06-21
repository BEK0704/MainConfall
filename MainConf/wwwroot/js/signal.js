var connection = new HubConnectionBuilder()
    .WithUrl(UrlBuilder.BuildEndpoint("/signalServer"))
    .ConfigureLogging(logging =>{
        logging.AddConsole();        // enable logging
    })
    .Build();

//Disable send button until connection is established
document.getElementById("startbutton").disabled = true;

connection.on("ReceiveMessage", function () {

    var qlev = $('#questionlevel');
    var qbody = $('#questionbody');
    var timer1 = document.getElementById('demo');
    var timer2 = document.getElementById('time2');


    $.ajax({
        type: 'Get',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        url: "/Home/Question",

        success: function (data) {
            if (data.part == 5) {
                timer1.style.visibility = "hidden";
                timer2.style.visibility = "hidden";
                if (data.usertype == 1) {
                    document.getElementById("nextbutton").disabled = true;
                }

            } else {

                timer1.style.visibility = "visible";
                timer2.style.visibility = "visible";
                if (data.usertype == 1) {
                    document.getElementById("startbutton").disabled = true;
                    document.getElementById("nextbutton").disabled = false;
                }
                qlev.empty();
                qlev.append(data.level);
                qbody.empty();
                qbody.append(data.question)
                countDownDate1 = new Date(data.alltime).getTime();
                countDownDate = new Date(data.qtime).getTime();
            }
        }
    });
});

connection.start().then(function () {
    document.getElementById("startbutton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("startbutton").addEventListener("click", function (event) {
   
    connection.invoke("SendMessage").catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

function loadData() {
    
   
        var qlev = $('#questionlevel');
        var qbody = $('#questionbody');
        var timer1 = document.getElementById('demo');
        var timer2 = document.getElementById('time2');


        $.ajax({
            type: 'Get',
            contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
            url: "/Home/Question",

            success: function (data) {
                if (data.part == 5) {
                    timer1.style.visibility = "hidden";
                    timer2.style.visibility = "hidden";
                    if (data.usertype == 1) {
                        document.getElementById("nextbutton").disabled = true;
                    }

                } else {

                    timer1.style.visibility = "visible";
                    timer2.style.visibility = "visible";
                    if (data.usertype == 1) {
                        document.getElementById("startbutton").disabled = true;
                        document.getElementById("nextbutton").disabled = false;
                    }
                    qlev.empty();
                    qlev.append(data.level);
                    qbody.empty();
                    qbody.append(data.question)
                    countDownDate1 = new Date(data.alltime).getTime();
                    countDownDate = new Date(data.qtime).getTime();
                }
            }
        });
}