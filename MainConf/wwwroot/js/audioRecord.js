var audio = document.querySelector('audio');
//var btnStopRecording = document.getElementById('marked');
function captureMicrophone(callback) {
   // btnReleaseMicrophone.disabled = false;
    if(microphone) {
        callback(microphone);
        return;
    }
    if(typeof navigator.mediaDevices === 'undefined' || !navigator.mediaDevices.getUserMedia) {
        alert('This browser does not supports WebRTC getUserMedia API.');
        if(!!navigator.getUserMedia) {
            alert('This browser seems supporting deprecated getUserMedia API.');
        }
    }
    navigator.mediaDevices.getUserMedia({
        audio: isEdge ? true : {
            echoCancellation: true
        }
    }).then(function(mic) {
        callback(mic);
    }).catch(function(error) {
        alert('Unable to capture your microphone. Please check console logs.');
        console.error(error);
    });
}
function replaceAudio(src) {
    var newAudio = document.createElement('audio');
    newAudio.controls = true;
    newAudio.autoplay = true;
    if(src) {
        newAudio.src = src;
    }
    
    var parentNode = audio.parentNode; 
    parentNode.innerHTML = '';
    parentNode.appendChild(newAudio);
    audio = newAudio;
}
function stopRecordingCallback() {
    replaceAudio(URL.createObjectURL(recorder.getBlob()));
   // btnStartRecording.disabled = false;
    setTimeout(function () {
        if (!audio.play) return;
        setTimeout(function () {
            if (!audio.play) return;
            audio.pause();
        }, 1000);

        audio.pause();
    }, 300);
    audio.pause();
    //btnDownloadRecording.disabled = false;
   /* if(isSafari) {
        click(btnReleaseMicrophone);
    }*/
}
function stopcand() {
    console.log("Audio yuklandi");
    recorder.stopRecording(stopRecordingCallback);
    setTimeout(function () { getfile(); }, 2000);
}
 function stopall() {
    //this.disabled = true;
     document.getElementById("marked").disabled = true;
     recorder.stopRecording(stopRecordingCallback);
     
    setTimeout(function () { getfile1(); }, 2000);
};
var isEdge = navigator.userAgent.indexOf('Edge') !== -1 && (!!navigator.msSaveOrOpenBlob || !!navigator.msSaveBlob);
var isSafari = /^((?!chrome|android).)*safari/i.test(navigator.userAgent);
var recorder; // globally accessible
var microphone;
//var btnStartRecording = document.getElementById('btn-start-recording');
//var btnStopRecording = document.getElementById('btn-stop-recording');
//var btnReleaseMicrophone = document.querySelector('#btn-release-microphone');
//var btnDownloadRecording = document.getElementById('btn-download-recording');

  function startrecoding() {
   // this.disabled = true;
   // document.querySelector('audio').style.display = 'none';
 //   this.style.border = '';
  //  this.style.fontSize = '';
    if (!microphone) {
        captureMicrophone(function(mic) {
            microphone = mic;
            if(isSafari) {
                replaceAudio();
                audio.muted = true;
                audio.srcObject = microphone;
                alert('Please click startRecording button again. First time we tried to access your microphone. Now we will record it.');
                return;
            }
            startrecoding();
        });
        return;
    }
    replaceAudio();
    audio.muted = true;
    audio.srcObject = microphone;
    var options = {
        type: 'audio',
        numberOfAudioChannels: isEdge ? 1 : 2,
        checkForInactiveTracks: true,
        bufferSize: 16384
    };
    if(isSafari || isEdge) {
        options.recorderType = StereoAudioRecorder;
    }
    if(navigator.platform && navigator.platform.toString().toLowerCase().indexOf('win') === -1) {
        options.sampleRate = 48000; // or 44100 or remove this line for default
    }
    if(isSafari) {
        options.sampleRate = 44100;
        options.bufferSize = 4096;
        options.numberOfAudioChannels = 2;
    }
    if(recorder) {
        recorder.destroy();
        recorder = null;
    }
    recorder = RecordRTC(microphone, options);
    recorder.startRecording();
   // btnStopRecording.disabled = false;
   // btnDownloadRecording.disabled = true;
      
};
/*btnStopRecording.onclick = function() {
    this.disabled = true;
    recorder.stopRecording(stopRecordingCallback);
};*/
function stoprecoding() {
   // this.disabled = true;
    recorder.stopRecording(stopRecordingCallback);
    getfile1();
};

/*btnReleaseMicrophone.onclick = function() {
    this.disabled = true;
    btnStartRecording.disabled = false;
    if(microphone) {
        microphone.stop();
        microphone = null;
    }
    if(recorder) {
        // click(btnStopRecording);
    }
};*/
/*btnDownloadRecording.onclick = function() {
    this.disabled = true;
    if(!recorder || !recorder.getBlob()) return;
    if(isSafari) {
        recorder.getDataURL(function(dataURL) {
            SaveToDisk(dataURL, getFileName('wav'));
        });
        return;
    }
    var blob = recorder.getBlob();
    var file = new File([blob], getFileName('wav'), {
        type: 'audio/wav'
    });
    invokeSaveAsDialog(file);
};*/
function getfile1() {
    // this.disabled = true;
    //recorder.stopRecording(stopRecordingCallback);
    //stoprecoding();
    console.log(document.getElementById("exppnfl").value + '_' + document.getElementById("canpnfl").value + '.wav');
    if (!recorder || !recorder.getBlob()) return;
    if (isSafari) {
        recorder.getDataURL(function (dataURL) {
            SaveToDisk(dataURL, document.getElementById("exppnfl").value + '_' + document.getElementById("canpnfl").value +'.wav');
        });
        return;
    }
    var blob = recorder.getBlob();
    var file = new File([blob], document.getElementById("exppnfl").value + '_' + document.getElementById("canpnfl").value + '.wav', {
        type: 'audio/wav'
    });
    console.log(file.name);
   // invokeSaveAsDialog(file, document.getElementById("exppnfl").value + '_' + document.getElementById("canpnfl").value + '.wav');
    var formData = new FormData();
    formData.append('files', file);
    $.ajax({
        url: "/Home/Addrecords1",
        type: 'POST',
        data: formData,
        processData: false,  // tell jQuery not to process the data
        contentType: false,  // tell jQuery not to set contentType
        success: function (result) {
            if (result.lets == true) { myfunction5(); } else {
               
                myfunction5();
            }
        },
        error: function (jqXHR) {
          
        },
        complete: function (jqXHR, status) {

        }
    });
};
function getfile() {
   // this.disabled = true;
    //recorder.stopRecording(stopRecordingCallback);
    if (!recorder || !recorder.getBlob()) return;
    if (isSafari) {
        recorder.getDataURL(function (dataURL) {
            SaveToDisk(dataURL, document.getElementById("exppnfl").value + '_' + document.getElementById("canpnfl").value + '.wav');
        });
        return;
    }
    var blob = recorder.getBlob();
    var file = new File([blob], document.getElementById("exppnfl").value + '_' + document.getElementById("canpnfl").value + '.wav', {
        type: 'audio/wav'
    });
    // invokeSaveAsDialog(file, document.getElementById("exppnfl").value + '_' + document.getElementById("canpnfl").value + '.wav');
    
    var formData = new FormData();
    formData.append('files', file); 
    $.ajax({
        url: "/Home/Addrecords",
        type: 'POST',
        data: formData,
        processData: false,  // tell jQuery not to process the data
        contentType: false,  // tell jQuery not to set contentType
        success: function (result) {
            checkaudiocand();
        },
        error: function (jqXHR) {
            invokeSaveAsDialog(file);
            self.location.replace('/Home/Index');
        },
        complete: function (jqXHR, status) {
            
        }
    });

    checkaudiocand();
};
function downloadaud() {
    var blob = recorder.getBlob();
    var file = new File([blob], document.getElementById("exppnfl").value + '_' + document.getElementById("canpnfl").value + '.wav', {
        type: 'audio/wav'
    });
    invokeSaveAsDialog(file, document.getElementById("exppnfl").value + '_' +document.getElementById("canpnfl").value + '.wav');
    self.location.replace('/Home/Index');
}
function Notdownload() {
    $.ajax({
        type: 'POST',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        url: "/Home/Writelog",
        success: function (data) {
            downloadaud();
            //self.location.replace('/Home/Index');
        }
    });
    
}
function gout() {
    self.location.replace('/Home/Index');
}
function checkaudiocand1() {
    $.ajax({
        type: 'GET',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: { parameter1: document.getElementById("exppnfl").value + '_' + document.getElementById("canpnfl").value },
        url: "/Home/Checkaudio",
        success: function (data) {
            if (data.lets == true) {
                $("#staticBackdrop3").modal('show');
               
            } else {
                $("#staticBackdrop2").modal('show');
            }
           
        }
    });
}
function checkaudiocand() {
    $.ajax({
        type: 'GET',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: { parameter1: document.getElementById("canpnfl").value },
        url: "/Home/Checkaudio",
        success: function (data) {
            if (data.lets == true) {
                $("#staticBackdrop3").modal('show');

            } else {
                $("#staticBackdrop2").modal('show');
            }

        }
    });
}

function click(el) {
    el.disabled = false; // make sure that element is not disabled
    var evt = document.createEvent('Event');
    evt.initEvent('click', true, true);
    el.dispatchEvent(evt);
}
function getRandomString() {
    if (window.crypto && window.crypto.getRandomValues && navigator.userAgent.indexOf('Safari') === -1) {
        var a = window.crypto.getRandomValues(new Uint32Array(3)),
            token = '';
        for (var i = 0, l = a.length; i < l; i++) {
            token += a[i].toString(36);
        }
        return token;
    } else {
        return (Math.random() * new Date().getTime()).toString(36).replace(/\./g, '');
    }
}
function getFileName(fileExtension) {
    var d = new Date();
    var year = d.getFullYear();
    var month = d.getMonth();
    var date = d.getDate();
    return 'RecordRTC-' + year+'_' + month+'_' + date + '_DTM' + '.' + fileExtension;
}
function SaveToDisk(fileURL, fileName) {
    // for non-IE
    if (!window.ActiveXObject) {
        var save = document.createElement('a');
        save.href = fileURL;
        save.download = fileName || 'unknown';
        save.style = 'display:none;opacity:0;color:transparent;';
        (document.body || document.documentElement).appendChild(save);
        if (typeof save.click === 'function') {
            save.click();
        } else {
            save.target = '_blank';
            var event = document.createEvent('Event');
            event.initEvent('click', true, true);
            save.dispatchEvent(event);
        }
        (window.URL || window.webkitURL).revokeObjectURL(save.href);
    }
    // for IE
    else if (!!window.ActiveXObject && document.execCommand) {
        var _window = window.open(fileURL, '_blank');
        _window.document.close();
        _window.document.execCommand('SaveAs', true, fileName || fileURL)
        _window.close();
    }
}