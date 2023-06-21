
function reg() {
    var ent=0.1;
    $.ajax({
        type: 'POST',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: { parameter1: document.getElementById("pnfl").value},
        url: "/Account/Entering1",
        success: function (data) {
            if (data.lets)
                ent = data.positions;
        }
    });

(async () => {
    await faceapi.nets.ssdMobilenetv1.load("/face/models");
    await faceapi.nets.faceRecognitionNet.load("/face/models");
    await faceapi.nets.faceLandmark68Net.load("/face/models");
  const input = document.getElementById("myImg");
  const result = await faceapi
    .detectSingleFace(input, new faceapi.SsdMobilenetv1Options())
    .withFaceLandmarks()
    .withFaceDescriptor();
  const displaySize = { width: input.width, height: input.height };
    
  const canvas = document.getElementById("myCanvas");
    faceapi.matchDimensions(canvas, displaySize);
    console.log(faceapi.matchDimensions(canvas, displaySize));
    const resizedDetections = faceapi.resizeResults(result, displaySize);

    const labeledFaceDescriptors = await detectFace();
    
  const faceMatcher = new faceapi.FaceMatcher(labeledFaceDescriptors, ent);
  if (result) {
    const bestMatch = faceMatcher.findBestMatch(result.descriptor);
      //const box = resizedDetections.detection.box;
     
      //const drawBox = new faceapi.draw.DrawBox(box, { label: bestMatch.label });
    //  document.body.append(bestMatch.label);
      //drawBox.draw(canvas);
      $.ajax({
          type: 'POST',
          contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
          data: { parameter1: document.getElementById("pnfl").value, parameter2: bestMatch.label, parameter3: resultb64, parameter4: bestMatch.distance.toFixed(2) },
          url: "/Account/Entering",
          success: function (data) {
              if (data.lets == true) {
                  $("#foot").empty();
                  $("#facetxt").empty();
                  $("#facetxt").append(data.positions);
                  $("#facemodal2").modal('show');
              } else
              {
                  $("#foot").empty();
                  $("#errors").empty();
                  if (data.positions < 5)
                      $("#errors").append("Shaxsni tasdiqlashda xatolik, iltimos yana qayta urinib ko'ring."); else
                      $("#errors").append("Urinishlar soni 5 ta bo'lganligi sababli, ma'lumot uchun adminstratorga murojat qiling.");
                  $("#facemodal1").modal('show');
              }
          }
      });
  }
})();}
async function detectFace() {
    const folder = document.getElementById("papka").value;
    const label = document.getElementById("pnfl").value;
  const descriptions = [];
  for (let i = 1; i <= 2; i++) {
      const img = await faceapi.fetchImage(
          `/face/` + folder + `/` + document.getElementById("pnfl").value + `/${i}.jpg`
    );
    const detection = await faceapi
      .detectSingleFace(img)
      .withFaceLandmarks()
      .withFaceDescriptor();
    descriptions.push(detection.descriptor);
  }
  return new faceapi.LabeledFaceDescriptors(label, descriptions);
}
