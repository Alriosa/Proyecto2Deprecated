let mediaURL = "https://res.cloudinary.com/oikos-store/image/upload/v1542776897/product_placeholder.svg";
let mediaQuantity = 0;
let mediaList = new Array(5);
let mediaToDeleteList = new Array();
let newMediaToRegister = new Array();

ToggleImages();
enableDeleteButtons();

function enableDeleteButtons() {
    document.querySelector("#btnDeleteMedia1").addEventListener("click",
        function() {
            RemoveMedia(0);
            ToggleImages();
        });

    document.querySelector("#btnDeleteMedia2").addEventListener("click",
        function() {
            RemoveMedia(1);
            ToggleImages();
        });

    document.querySelector("#btnDeleteMedia3").addEventListener("click",
        function() {
            RemoveMedia(2);
            ToggleImages();
        });

    document.querySelector("#btnDeleteMedia4").addEventListener("click",
        function() {
            RemoveMedia(3);
            ToggleImages();
        });

    document.querySelector("#btnDeleteMedia5").addEventListener("click",
        function() {
            RemoveMedia(4);
            ToggleImages();
        });

}

function AddNewMediaFromUpdate(url) {
    newMediaToRegister.push(url);
    AddMedia(url);
}

function AddMedia(url) {
    if (mediaQuantity < 5) {
        mediaList[mediaQuantity] = url;
        ToggleImages();
        mediaQuantity++;
    }
}

function markMediaToSoftDelete(url) {
    mediaToDeleteList.push(url);
}

function RemoveMedia(index) {
    markMediaToSoftDelete(mediaList[index]);
    mediaList.splice(index, 1);
    mediaQuantity--;
    ToggleImages();
    ToggleButton();
}

function ToggleImages() {
    switch (mediaQuantity) {
    case 5:
        document.querySelector('#btnDeleteMedia5').classList.remove("invisible");
        document.querySelector('#imgProductMedia5').src = mediaList[4];
        break;
    case 4:
        document.querySelector('#txtBtnCloudinary').innerHTML = 'Agregar imagen ' + 5;
        document.querySelector('#divImgProduct5').classList.remove("invisible");
        document.querySelector('#imgProductMedia5').classList.remove("invisible");

        document.querySelector('#btnDeleteMedia5').classList.add("invisible");
        document.querySelector('#divImgProduct4').classList.remove("invisible");
        document.querySelector('#btnDeleteMedia4').classList.remove("invisible");

        document.querySelector('#imgProductMedia5').src = mediaURL;
        document.querySelector('#imgProductMedia4').src = mediaList[3];
        document.querySelector('#imgProductMedia3').src = mediaList[2];
        document.querySelector('#imgProductMedia2').src = mediaList[1];
        document.querySelector('#imgProductMedia1').src = mediaList[0];
        break;
    case 3:
        document.querySelector('#txtBtnCloudinary').innerHTML = 'Agregar imagen ' + 4;
        document.querySelector('#divImgProduct5').classList.add("invisible");
        document.querySelector('#imgProductMedia5').classList.add("invisible");
        document.querySelector('#divImgProduct4').classList.remove("invisible");
        document.querySelector('#imgProductMedia4').classList.remove("invisible");

        document.querySelector('#btnDeleteMedia4').classList.add("invisible");
        document.querySelector('#btnDeleteMedia3').classList.remove("invisible");

        document.querySelector('#imgProductMedia4').src = mediaURL;
        document.querySelector('#imgProductMedia3').src = mediaList[2];
        document.querySelector('#imgProductMedia2').src = mediaList[1];
        document.querySelector('#imgProductMedia1').src = mediaList[0];
        break;
    case 2:
        document.querySelector('#txtBtnCloudinary').innerHTML = 'Agregar imagen ' + 3;
        document.querySelector('#divImgProduct4').classList.add("invisible");
        document.querySelector('#imgProductMedia4').classList.add("invisible");
        document.querySelector('#btnDeleteMedia3').classList.add("invisible");
        document.querySelector('#btnDeleteMedia2').classList.remove("invisible");

        document.querySelector('#imgProductMedia3').src = mediaURL;
        document.querySelector('#imgProductMedia2').src = mediaList[1];
        document.querySelector('#imgProductMedia1').src = mediaList[0];
        break;
    case 1:
        document.querySelector('#txtBtnCloudinary').innerHTML = 'Agregar imagen ' + 2;
        document.querySelector('#btnDeleteMedia2').classList.add("invisible");
        document.querySelector('#btnDeleteMedia1').classList.remove("invisible");

        document.querySelector('#imgProductMedia2').src = mediaURL;
        document.querySelector('#imgProductMedia1').src = mediaList[0];
        break;
    case 0:
        document.querySelector('#txtBtnCloudinary').innerHTML = 'Agregar imagen ' + 1;
        document.querySelector('#btnDeleteMedia1').classList.add("invisible");
        document.querySelector('#btnDeleteMedia2').classList.add("invisible");
        document.querySelector('#btnDeleteMedia3').classList.add("invisible");
        document.querySelector('#btnDeleteMedia4').classList.add("invisible");
        document.querySelector('#btnDeleteMedia5').classList.add("invisible");
        document.querySelector('#divImgProduct5').classList.add("invisible");
        document.querySelector('#divImgProduct4').classList.add("invisible");
        document.querySelector('#imgProductMedia5').classList.add("invisible");
        document.querySelector('#imgProductMedia4').classList.add("invisible");

        document.querySelector('#imgProductMedia1').src = mediaURL;
        document.querySelector('#imgProductMedia2').src = mediaURL;
        document.querySelector('#imgProductMedia3').src = mediaURL;
        document.querySelector('#imgProductMedia4').src = mediaURL;
        document.querySelector('#imgProductMedia5').src = mediaURL;
        break;
    }
}

function ToggleButton() {
    if (mediaQuantity == 5) {
        document.querySelector('#divBtnCloudinary').classList.add("invisible");

    } else {
        document.querySelector('#divBtnCloudinary').classList.remove("invisible");
    }
}

function showUploadWidget() {
    cloudinary.openUploadWidget({
            cloudName: "oikos-store",
            uploadPreset: "products",
        sources: ["local", "url", "camera", "image_search"],
            googleApiKey: "AIzaSyDtbZuXwQdY9PS0Le4TwEHKytGS9jKWPeY",
            showAdvancedOptions: false,
            async: false,
//            cropping: "server",
            defaultSource: "local",
            max_files: 5,
            multiple: true,
            styles:
            {
                palette: {
                    window: "#FFFFFF",
                    windowBorder:
                        "#90A0B3",
                    tabIcon:
                        "#24A2B6",
                    menuIcons:
                        "#5A616A",
                    textDark:
                        "#000000",
                    textLight:
                        "#FFFFFF",
                    link:
                        "#24A2B6",
                    action:
                        "#FF620C",
                    inactiveTabIcon:
                        "#0E2F5A",
                    error:
                        "#F44235",
                    inProgress:
                        "#0078FF",
                    complete:
                        "#20B832",
                    sourceBg:
                        "#E4EBF1"
                },
                fonts: {
                    default:
                        null,
                    "'Source Sans Pro', Helvetica, sans-serif":
                    {
                        url: "https://fonts.googleapis.com/css?family=Source+Sans+Pro",
                        active:
                            true
                    }
                }
            }
        },
        (err, result) => {
            if (!err && result.event === "success") {
                console.log("Upload Widget event - ", result);
//                var imageURL = result.info.secure_url;
                var imageURL = "https://res.cloudinary.com/oikos-store/image/upload/ar_1:1,c_pad,h_800,q_auto:good,r_0,w_800/" + result.info.path;
                if (document.querySelector('#whoAmI').innerHTML === "update" || document.querySelector('#whoAmI').innerHTML === "list") {
                    AddNewMediaFromUpdate(imageURL);
                } else {
                    AddMedia(imageURL);
                }
                ToggleButton();
                ToggleImages();
            }
        });
}