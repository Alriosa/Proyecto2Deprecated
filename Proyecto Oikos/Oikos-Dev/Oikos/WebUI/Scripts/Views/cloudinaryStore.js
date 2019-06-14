let mediaURL = "";
document.querySelector('#txtLogo');
document.querySelector('#imgLogoPlaceholder');
document.querySelector('#txtLogo').value = mediaURL;
document.querySelector('#imgStoreLogo').src = "https://res.cloudinary.com/oikos-store/image/upload/v1542338325/shop_logo_placeholder.svg";


function showUploadWidget(multiple) {
    cloudinary.openUploadWidget({
        cloudName: "oikos-store",
        uploadPreset: "oikosUploads",
        sources: ["local", "url", "camera", "image_search"],
        googleApiKey: "AIzaSyD3g4htsFZcxWmB3wphhJAaTsakT1JFl0s",
        showAdvancedOptions: false,
//        cropping: true,
//        multiple: multiple,
        defaultSource: "local",
        styles: {
            palette: {
                window: "#FFFFFF",
                windowBorder: "#90A0B3",
                tabIcon: "#24A2B6",
                menuIcons: "#5A616A",
                textDark: "#000000",
                textLight: "#FFFFFF",
                link: "#24A2B6",
                action: "#FF620C",
                inactiveTabIcon: "#0E2F5A",
                error: "#F44235",
                inProgress: "#0078FF",
                complete: "#20B832",
                sourceBg: "#E4EBF1",
                maxFiles: 5
            },
            fonts: {
                default: null,
                "'Source Sans Pro', Helvetica, sans-serif": {
                    url: "https://fonts.googleapis.com/css?family=Source+Sans+Pro",
                    active: true
                }
            }
        }
    }, (err, result) => {
        if (!err && result.event==="success") {
            console.log("Upload Widget event - ", result);
            mediaURL = result.info.secure_url;
            document.querySelector('#txtLogo').value = mediaURL;
            document.querySelector('#imgStoreLogo').src = mediaURL;
        }
    });
}
