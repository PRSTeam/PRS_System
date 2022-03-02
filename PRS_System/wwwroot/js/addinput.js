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
            $(wrapper).append('<div><input type="text" name="mytext[]" placeholder="�����Ԫ�"/><a href="#" class="delete">Delete</a></div>'); //add input box
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
            var text = `<div><label>�����Թ���</label><input type="text" id="Productdata[[string]].NameProduct" Name="Productdata[[string]].NameProduct" placeholder="������� 50 ��.">
                            <label>�ӹǹ�Թ���</label><input type="text" id="Productdata[[string]].AmtProduct" Name="Productdata[[string]].AmtProduct" placeholder="99">
                            <label>˹����Թ���</label><input type="text" id="Productdata[[string]].Unit" Name="Productdata[[string]].Unit" placeholder="�ѹ">
                            <label>�Ҥҵ��˹���</label><input type="text" id="Productdata[[string]].Price_Per_Piece" Name="Productdata[[string]].Price_Per_Piece" placeholder="100"/><a href="#" class="delete">Delete</a></div>`;

            text = text.replaceAll('[string]', y.toString());
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