 const swiper = new Swiper('.swiper', {
     loop: true,
     slidesPerView: 'auto',
     navigation: {
         nextEl: '.swiper-button-next',
         prevEl: '.swiper-button-prev',
     },
     pagination: {
         el: '.swiper-pagination',
         type: 'bullets',
         clickable: true,
     },
      autoplay: {
          delay: 7000,
          disableOnInteraction: false,
      },
      //  mousewheel: {
      //    invert: true,
      //},
 });
var del = document.querySelectorAll(".sw-icon");

function Check() {
    var name = document.getElementById("name").value;
    var email = document.getElementById("email").value;
    var message = document.getElementById("message").value;

    if (name != "" && email != "" && message != "") {
        document.querySelector(".application-button").removeAttribute("disabled")
    } else {
        document.querySelector(".application-button").setAttribute("disabled", "true")
    }
}

async function CreateApplication() {
    const response = await fetch(`/api/application/items`, {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify(
            {
                userFullName: document.getElementById("name").value,
                userEmail: document.getElementById("email").value,
                userMessage: document.getElementById("message").value
            },
        )
    });

    await response.text();

    if (response.ok) {
        document.getElementById("email").value = "";
        document.getElementById("message").value = "";
    }
    console.log(response)
}

document.querySelector('.file').addEventListener("change", function () {
    var file = document.querySelector('.file').files[0];
    var reader = new FileReader();
    reader.onload = function (e) {
        var image = document.createElement("img");
        image.src = e.target.result;
        image.classList.add("sw-img-create")
        document.querySelector(".create").classList.remove("none");

        var img = document.querySelector('.sw-img-create');
        if (img != null) {
            document.querySelector(".create").classList.remove("none");
            img.remove();
        }
        document.querySelector(".create").appendChild(image);
    }
    reader.readAsDataURL(file);
});

document.querySelector(".sw-save").addEventListener("click", async function () {
    var image = document.querySelector(".sw-img-create");

    const response = await fetch(`/api/image/items`, {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify(
            {
                image: image.src
            },
        )
    });

    await response.text();

    if (response.ok) {
        window.location.href = "/MyHome/Index";
    }
    console.log(response)
});

del.forEach(function (e) {
    e.addEventListener("click", async function (i) {
        var parent = i.target.closest('.sw-image');
        var id = parent.querySelector(".image-id").textContent;

        const response = await fetch(`/api/image/items/${id}`, {
            method: "DELETE",
            headers: { "Accept": "application/json", "Content-Type": "application/json" },
        });

        await response.text();

        if (response.ok) {
            window.location.href = "/MyHome/Index";
        }

        console.log(response)
    });
});