$(document).ready(function () {
    var max_fields = 10;
    var wrapper = $(".container1");
    var add_button = $(".add_form_field");
    var max_object = 20;
    var object = $(".container2");
    var add_object = $(".add_form_object");

    var x = 1;
    $(add_button).click(function (e) {
        e.preventDefault();
        if (x < max_fields) {
            x++;
            $(wrapper).append('<div><input type="text" name="mytext[]" placeholder="รหัสวิชา"/><a href="#" class="delete">Delete</a></div>'); //add input box
        } else {
            alert('You Reached the limits')
        }
    });

    $(wrapper).on("click", ".delete", function (e) {
        e.preventDefault();
        $(this).parent('div').remove();
        x--;
    })

    var y = 1;
    $(add_object).click(function (e) {
        e.preventDefault();
        if (y < max_object) {
            var text = '<div><label>ชื่อสินค้า</label><input type="text" id="nameProduct" placeholder="บิ๊กเกอร์ 50 มล."><label>จำนวนสินค้า</label><input type="text" id="numProduct" placeholder="99"><label>หน่วยสินค้า</label><input type="text" id="unitProduct" placeholder="อัน"><label>ราคาต่อหน่วย</label><input type="text" id="priceUnit" placeholder="100"/><a href="#" class="delete">Delete</a></div>';
            y++;
            $(object).append(utf8.encode(text)); //add input box
        } else {
            alert('You Reached the limits')
        }
    });

    $(object).on("click", ".delete", function (e) {
        e.preventDefault();
        $(this).parent('div').remove();
        y--;
    })
});