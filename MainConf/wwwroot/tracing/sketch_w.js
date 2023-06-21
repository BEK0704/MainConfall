var capture;
var tracker
var w = 640,
    h = 480;
var tim = 0;
var rig = 0, lef = 0, cen = 0;

var zeroPosition = 0,
	fourteenPosition = 0;

var ball = {};
var soundFile1, soundFile2;
var Clock = {
	totalSeconds: 0,

	start: function () {
		var self = this;

		this.interval = setInterval(function () {
			self.totalSeconds += 1;
			tim = tim + 1;
			if (tim == 15) {
				//recognition.stop();
				//$("#ogoh").empty();
				//$("#ogoh").append("<span class='col-12'>Hurmatli nomzod imtihon qoidalarini buzmoqdasiz. Manitorga to'g'ri qarang. Qoidalarga amal qilmaslik imtihondan chetlashishga sabab bo'ladi.</span><center><img style='max-width:50%;' src='/tracing/lk1.gif' /><center>");
				//$("#exampleModal4").modal('show');
				showToast("<span class='col-12'>Hurmatli nomzod imtihon qoidalarini buzmoqdasiz. Manitorga to'g'ri qarang. Qoidalarga amal qilmaslik imtihondan chetlashishga sabab bo'ladi.</span><center><img style='max-width:50%;' src='/tracing/lk1.gif' /></center>");
			}
			if (tim == 20) {
				Contwriting(2);
				//	recognition.stop();
				//	$("#ogoh").empty();
				//	$("#ogoh").append("<span class='col-12'>Hurmatli nomzod imtihon qoidalarini buzmoqdasiz. Manitorga to'g'ri qarang. Qoidalarga amal qilmaslik imtihondan chetlashishga sabab bo'ladi.</span><center><img style='max-width:50%;' src='/tracing/lk1.gif' /><center>");
				//("#exampleModal4").modal('show');
				showToast("<span class='col-12'>Hurmatli nomzod imtihon qoidalarini buzmoqdasiz. Manitorga to'g'ri qarang. Qoidalarga amal qilmaslik imtihondan chetlashishga sabab bo'ladi.</span><center><img style='max-width:50%;' src='/tracing/lk1.gif' /></center>");
				tim = 0;
				console.log(" look ishladi");
			}
		}, 1000);
	},

	pause: function () {
		clearInterval(this.interval);
		delete this.interval;
	},

	resume: function () {
		if (!this.interval) this.start();
	}
};

Clock.start();

function preload() {
  soundFormats( 'ogg');
  soundFile1 = loadSound('/tracing/sound1.ogg');
  soundFile2 = loadSound('/tracing/sound2.ogg');
}

function setup() {
    capture = createCapture({
        audio: false,
        video: {
            width: w,
            height: h
			
        }
    }, function() {
        console.log('capture ready.')
    });
    capture.elt.setAttribute('playsinline', '');
	capture.elt.setAttribute('id', 'tracingVideo');
    createCanvas(w, h);
    capture.size(w, h);
    capture.hide();

    colorMode(HSB);

    tracker = new clm.tracker();
    tracker.init();
    tracker.start(capture.elt);
}

function draw() {
    image(capture, 0, 0, w, h);
    var positions = tracker.getCurrentPosition();
	
	// Full Face
	noFill();
	stroke(255);
	beginShape();
	for (var i = 0; i < positions.length - 56; i++) {
		if (i == 0 ) { zeroPosition = positions[i][1]; }
		if (i == 14 ) { fourteenPosition = positions[i][1]; }
		vertex(positions[i][0], positions[i][1]);
	}
	endShape();
	
	if ( zeroPosition <= fourteenPosition + 40 && zeroPosition >= fourteenPosition - 40 ) {
		// Center
		if (cen == 0) {
			rig = 0;
			cen = 1;
			lef = 0;
			Clock.pause();
			tim = 0;
		}
	} else if (zeroPosition >= fourteenPosition) {
		//right
		if (rig == 0) {
			rig = 1;
			cen = 0;
			lef = 0;
			Clock.resume();
		}
		/*if (!soundFile1.isPlaying()){
        soundFile1.play();}*/
	} else if ( zeroPosition <= fourteenPosition ) {
		// Left
		if (lef == 0) {
			rig = 0;
			cen = 0;
			lef = 1;
			Clock.resume();
		}
       /* if (!soundFile2.isPlaying()){
		soundFile2.play();}*/
	}else {
		
		console.log('Not detect');
		
	}
	
	// Eyebrow Left
	noFill();
	stroke(255);
	beginShape();
	for (var i = 19; i < positions.length - 48; i++) {
		vertex(positions[i][0], positions[i][1]);
	}
	endShape();
	
	// Eyebrow Right
	noFill();
	stroke(255);
	beginShape();
	for (var i = 15; i < positions.length - 52; i++) {
		vertex(positions[i][0], positions[i][1]);
	}
	endShape();
	
	// Mouth
	noFill();
	stroke(255);
	beginShape();
	for (var i = 44; i < positions.length - 15; i++) {
		vertex(positions[i][0], positions[i][1]);
		if ( i == positions.length - 16 ) { vertex(positions[44][0], positions[44][1]); }
	}
	endShape();
	
	// Nose
	noFill();
	stroke(255);
	beginShape();
	for (var i = 33; i < positions.length - 30; i++) {
		vertex(positions[i][0], positions[i][1]);
		if ( i == positions.length - 31 ) { vertex(positions[33][0], positions[33][1]); }
	}
	endShape();
	
	// Left Eye
	noFill();
	stroke(255);
	beginShape();
	for (var i = 23; i < positions.length - 44; i++) {
		vertex(positions[i][0], positions[i][1]);
		if ( i == positions.length - 45 ) { vertex(positions[23][0], positions[23][1]); }
	}
	endShape();
	
	// Right Eye
	noFill();
	stroke(255);
	beginShape();
	for (var i = 28; i < positions.length - 39; i++) {
		vertex(positions[i][0], positions[i][1]);
		if ( i == positions.length - 40 ) { vertex(positions[28][0], positions[28][1]); }
	}
	endShape();
}
