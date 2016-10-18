$.fn.serializeObject = function () {
    var o = {};
    var a = this.serializeArray();
    $.each(a, function () {
        if (o[this.name] !== undefined) {
            if (!o[this.name].push) {
                o[this.name] = [o[this.name]];
            }
            o[this.name].push(this.value || '');
        } else {
            o[this.name] = this.value || '';
        }
    });
    return o;
};
$(function () {
    $(".crear-coupon").click(function (event) {
        event.preventDefault();
        console.log("crear cupón");
        var form = $("#form-agregar-coupon");

        if (form.valid())
        {
            modalToClose = $("#crear_coupon");
            var formData = $("#form-agregar-coupon").serializeObject();

            formData.Status = (formData.Status == "Activo") ? true : false;

            $.ajax({
                type: 'POST',
                url: "/Coupon/CreateCoupon",
                data: formData,
                success: function (data) {
                    if (data.success) {
                        verifCloseModal = true;
                        mostrarMensaje(form, "userMensaje", "Cupón registrado", "alert-warning");
                        window.location.reload(false);
                    } else {
                        mostrarMensaje(form, "userMensaje", "Error al registrar cupón intentelo de nuevo", "alert-warning");
                    }
                }, error: function (error) {
                    mostrarMensaje(form, "userMensaje", "Error al registrar cupón intentelo de nuevo", "alert-warning");
                }, complete: function (data) {
                    console.log(data);
                }
            });
        }
        
    });
    $(".editar-coupon-button").click(function (event) {

        var countCheckbox = $("#tabla-coupon").find('input:checkbox:checked').length;
        if (countCheckbox == 0) {
            alert("Debe seleccionar un cupón para editar");
            return false;
        }
        console.log("editar cupón");
        var form = $("#form-editar-coupon");
        var formData = {};

        formData.id = actualEdicion;

        if (actualEdicion != null) {
            $("#editar-id-coupon").val(actualEdicion);

            $.ajax({
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                type: 'GET',
                url: '/Coupon/GetCoupon',
                data: formData,
                success: function (data) {
                    console.log("success data");
                    console.log(data);
                    if (data) {
                        form.find("#Name").val(data.Name);
                        form.find("#Quantity").val(data.Quantity);
                        form.find("#Discount").val(data.Discount);
                        form.find("#fecha_cupon_inicio_edit").val(data.From);
                        form.find("#fecha_cupon_vigencia_edit").val(data.To);
                        form.find("#Created_by").val(data.Created_by);
                        form.find("#Created_by").trigger("chosen:updated");
                        //form.find("#fecha_cupon_creacion_edit").val(data.Slu);
                        var statusname = "Activo"
                        if (!data.Status)
                            statusname = "Inactivo"
                        form.find("#Status").val(statusname);

                        $("#editar_coupon").modal("show");
                    }
                }, error: function (error) {
                    mostrarMensaje(form, "userMensaje", "Error al registrar cupón intentelo de nuevo", "alert-warning");
                }, statusCode: {
                    404: function () {
                        alert("page not found");
                    }
                }, complete: function (data) {
                    console.log(data);
                }
            });
        }

    });
    $(".modificar-coupon").click(function (event) {
        event.preventDefault();
        modalToClose = $("#editar_coupon");
        console.log("modificar cupón");
        var form = $("#form-editar-coupon");

        if (form.valid())
        {
            var formData = $("#form-editar-coupon").serializeObject();

            formData.status = (formData.status == "Activo") ? true : false;

            $.ajax({
                type: 'POST',
                url: '/Coupon/UpdateCoupon',
                data: formData,
                success: function (data) {
                    if (data.success) {
                        mostrarMensaje(form, "userMensaje", "Cupón modificado", "alert-warning");
                        verifCloseModal = true;
                        actualElement.closest("tr").find(".tname").text(form.find("#Name").val());
                        actualElement.closest("tr").find(".tquantity").text(form.find("#Quantity").val());
                        actualElement.closest("tr").find(".tdiscount").text(form.find("#Discount").val());
                        actualElement.closest("tr").find(".tfrom").text(form.find("#fecha_cupon_inicio_edit").val());
                        actualElement.closest("tr").find(".tto").text(form.find("#fecha_cupon_vigencia_edit").val());
                        //actualElement.closest("tr").find(".tcreated").text(form.find("#secure").val());
                        //actualElement.closest("tr").find(".tdatecreated").text(form.find("#slu").val());
                        actualElement.closest("tr").find(".tstatus").text(form.find("#status").val());
                    } else {
                        mostrarMensaje(form, "userMensaje", "Error al modificar cupón intentelo de nuevo", "alert-warning");
                    }
                }, error: function (error) {
                    mostrarMensaje(form, "userMensaje", "Error al modificar cupón intentelo de nuevo", "alert-warning");
                }, complete: function (data) {
                    console.log(data);
                }
            });
        }
        
    });
    $(".eliminar-coupon").click(function (event) {
        var countCheckbox = $("#tabla-coupon").find('input:checkbox:checked').length;
        console.log(countCheckbox);
        var mensaje = "Esta seguro que desea eliminar los cupones seleccionados?";
        if (countCheckbox == 1) {
            var mensaje = "Esta seguro que desea eliminar el cupón " + actualName + "?";
        } else if (countCheckbox == 0) {
            alert("Debe seleccionar al menos un cupón para editar");
            return false;
        }
        if (confirm(mensaje)) {
            var formData = {};
            formData.id = actualEdicion;
            if (countCheckbox > 1) {
                $("#tabla-coupon").find('input:checkbox:checked').each(function () {
                    formData = {};
                    formData.id = $(this).attr("data-id");
                    deleteCoupon(formData, $(this));
                })
            } else if (countCheckbox == 1) {
                deleteCoupon(formData, actualElement);
            }
        }
    });
    var actualEdicion = null;
    var actualName = "";
    var actualElement = null;
    $(document).on("change", ".select-coupon", function (event) {
        if ($(this).is(":checked")) {
            actualElement = $(this);
            actualEdicion = $(this).attr("data-id");
            actualName = $(this).closest("tr").find(".tname").text();
        }
    });

    //--------------------------------CODIGOS------------------------------------

    $(".crear-code").click(function (event) {
        event.preventDefault();
        console.log("crear código");
        var form = $("#form-agregar-code");

        if (form.valid())
        {
            modalToClose = $("#crear_code");
            var formData = $("#form-agregar-code").serializeObject();

            formData.Status = (formData.Status == "Activo") ? true : false;

            $.ajax({
                type: 'POST',
                url: "/Code/CreateCode",
                data: formData,
                success: function (data) {
                    if (data.success) {
                        verifCloseModal = true;
                        mostrarMensaje(form, "userMensaje", "Código registrado", "alert-warning");
                        window.location.reload(false);
                    } else {
                        mostrarMensaje(form, "userMensaje", "Error al registrar código intentelo de nuevo", "alert-warning");
                    }
                }, error: function (error) {
                    mostrarMensaje(form, "userMensaje", "Error al registrar código intentelo de nuevo", "alert-warning");
                }, complete: function (data) {
                    console.log(data);
                }
            });
        }
        
    });
    $(".editar-code-button").click(function (event) {

        var countCheckbox = $("#tabla-code").find('input:checkbox:checked').length;
        if (countCheckbox == 0) {
            alert("Debe seleccionar un código para editar");
            return false;
        }
        console.log("editar código");
        var form = $("#form-editar-code");
        var formData = {};

        formData.id = actualEdicion;

        if (actualEdicion != null) {
            $("#editar-id-code").val(actualEdicion);

            $.ajax({
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                type: 'GET',
                url: '/Code/GetCode',
                data: formData,
                success: function (data) {
                    console.log("success data");
                    console.log(data);
                    if (data) {
                        form.find("#Code").val(data.Code);
                        form.find("#Amount").val(data.Amount);
                        form.find("#fecha_codigo_inicio_edit").val(data.From);
                        form.find("#fecha_codigo_vigencia_edit").val(data.To);
                        form.find("#Quantity").val(data.Quantity);
                        form.find("#Created_by").val(data.Created_by);
                        form.find("#Created_by").trigger("chosen:updated");
                        //form.find("#fecha_cupon_creacion_edit").val(data.Slu);
                        var statusname = "Activo"
                        if (!data.Status)
                            statusname = "Inactivo"
                        form.find("#Status").val(statusname);

                        $("#editar_code").modal("show");
                    }
                }, error: function (error) {
                    mostrarMensaje(form, "userMensaje", "Error al registrar código intentelo de nuevo", "alert-warning");
                }, statusCode: {
                    404: function () {
                        alert("page not found");
                    }
                }, complete: function (data) {
                    console.log(data);
                }
            });
        }

    });
    $(".modificar-code").click(function (event) {
        event.preventDefault();
        modalToClose = $("#editar_code");
        console.log("modificar código");
        var form = $("#form-editar-code");

        if (form.valid())
        {
            var formData = $("#form-editar-code").serializeObject();

            formData.status = (formData.status == "Activo") ? true : false;

            $.ajax({
                type: 'POST',
                url: '/Code/UpdateCode',
                data: formData,
                success: function (data) {
                    if (data.success) {
                        mostrarMensaje(form, "userMensaje", "Código modificado", "alert-warning");
                        verifCloseModal = true;
                        actualElement.closest("tr").find(".tcode").text(form.find("#Code").val());
                        actualElement.closest("tr").find(".tamount").text(form.find("#Amount").val());
                        actualElement.closest("tr").find(".tfrom").text(form.find("#fecha_codigo_inicio_edit").val());
                        actualElement.closest("tr").find(".tto").text(form.find("#fecha_codigo_vigencia_edit").val());
                        actualElement.closest("tr").find(".tquantity").text(form.find("#Quantity").val());
                        //actualElement.closest("tr").find(".tcreated").text(form.find("#secure").val());
                        //actualElement.closest("tr").find(".tdatecreated").text(form.find("#slu").val());
                        actualElement.closest("tr").find(".tstatus").text(form.find("#status").val());
                    } else {
                        mostrarMensaje(form, "userMensaje", "Error al modificar código intentelo de nuevo", "alert-warning");
                    }
                }, error: function (error) {
                    mostrarMensaje(form, "userMensaje", "Error al modificar código intentelo de nuevo", "alert-warning");
                }, complete: function (data) {
                    console.log(data);
                }
            });
        }
        
    });
    $(".eliminar-code").click(function (event) {
        var countCheckbox = $("#tabla-code").find('input:checkbox:checked').length;
        console.log(countCheckbox);
        var mensaje = "Esta seguro que desea eliminar los códigos seleccionados?";
        if (countCheckbox == 1) {
            var mensaje = "Esta seguro que desea eliminar el código " + actualCode + "?";
        } else if (countCheckbox == 0) {
            alert("Debe seleccionar al menos un código para editar");
            return false;
        }
        if (confirm(mensaje)) {
            var formData = {};
            formData.id = actualEdicion;
            if (countCheckbox > 1) {
                $("#tabla-code").find('input:checkbox:checked').each(function () {
                    formData = {};
                    formData.id = $(this).attr("data-id");
                    deleteCoupon(formData, $(this));
                })
            } else if (countCheckbox == 1) {
                deleteCoupon(formData, actualElement);
            }
        }
    });
    var actualEdicion = null;
    var actualName = "";
    var actualElement = null;
    $(document).on("change", ".select-code", function (event) {
        if ($(this).is(":checked")) {
            actualElement = $(this);
            actualEdicion = $(this).attr("data-id");
            actualCode = $(this).closest("tr").find(".tcode").text();
        }
    });

    //-------------------------JQuery Validate----------------------
    $('#form-agregar-coupon').validate({
        rules: {
            Name: {
                required: true
            },
            Quantity: {
                required: true
            },
            Discount: {
                required: true
            },
            From: {
                required: true
            },
            To: {
                required: true
            },
            "Created_by": {
                required: true
            }
        },
        highlight: function (element) {
            $(element).closest('.form-group').addClass('has-error');
        },
        unhighlight: function (element) {
            $(element).closest('.form-group').removeClass('has-error');
        },
        errorElement: 'span',
        errorClass: 'help-block',
        errorPlacement: function (error, element) {
            if (element.parent('.input-group').length) {
                error.insertAfter(element.parent());
            } else {
                error.insertAfter(element);
            }
        }
    });

    $('#form-editar-coupon').validate({
        rules: {
            Name: {
                required: true
            },
            Quantity: {
                required: true
            },
            Discount: {
                required: true
            },
            From: {
                required: true
            },
            To: {
                required: true
            },
            "Created_by": {
                required: true
            }
        },
        highlight: function (element) {
            $(element).closest('.form-group').addClass('has-error');
        },
        unhighlight: function (element) {
            $(element).closest('.form-group').removeClass('has-error');
        },
        errorElement: 'span',
        errorClass: 'help-block',
        errorPlacement: function (error, element) {
            if (element.parent('.input-group').length) {
                error.insertAfter(element.parent());
            } else {
                error.insertAfter(element);
            }
        }
    });

    $('#form-agregar-code').validate({
        rules: {
            Code: {
                required: true
            },
            Quantity: {
                required: true
            },
            Amount: {
                required: true
            },
            From: {
                required: true
            },
            To: {
                required: true
            },
            "Created_by": {
                required: true
            }
        },
        highlight: function (element) {
            $(element).closest('.form-group').addClass('has-error');
        },
        unhighlight: function (element) {
            $(element).closest('.form-group').removeClass('has-error');
        },
        errorElement: 'span',
        errorClass: 'help-block',
        errorPlacement: function (error, element) {
            if (element.parent('.input-group').length) {
                error.insertAfter(element.parent());
            } else {
                error.insertAfter(element);
            }
        }
    });

    $('#form-editar-code').validate({
        rules: {
            Code: {
                required: true
            },
            Quantity: {
                required: true
            },
            Amount: {
                required: true
            },
            From: {
                required: true
            },
            To: {
                required: true
            },
            "Created_by": {
                required: true
            }
        },
        highlight: function (element) {
            $(element).closest('.form-group').addClass('has-error');
        },
        unhighlight: function (element) {
            $(element).closest('.form-group').removeClass('has-error');
        },
        errorElement: 'span',
        errorClass: 'help-block',
        errorPlacement: function (error, element) {
            if (element.parent('.input-group').length) {
                error.insertAfter(element.parent());
            } else {
                error.insertAfter(element);
            }
        }
    });

    $('.pagination-coupons').pagination({
        items: $('#total-coupons').val(),
        itemsOnPage: 10,
        cssStyle: 'light-theme',
        onPageClick: function (pageNumber, event) {
            event.preventDefault();

            $("#tabla-coupon").html('');
            pageLoading(true);
            var formData = { idPagination: $('#idpagination-coupons').val(), page: pageNumber };
            var cadhtml = "";
            var couponsresult = "";

            $.ajax({
                type: 'POST',
                url: '@Url.Action("PaginationAjax", "Coupon")',
                data: formData,
                success: function (result) {

                    couponsresult = "";

                    for (var i = 0; i < result.Coupons.length; i++) {

                        var couponv = result.Coupons[i];

                        cadhtml = "";
                        cadhtml += "<tr>";
                        cadhtml += "    <td><input type=\"checkbox\" data-id=\"" + couponv.Id + "\" class=\"select-coupon\" \/><\/td>";
                        cadhtml += "    <td class=\"tname\">" + couponv.Name + "<\/td>";
                        cadhtml += "    <td class=\"tquantity\">" + couponv.Quantity + "<\/td>";
                        cadhtml += "    <td class=\"tdiscount\">" + couponv.Discount % +"<\/td>";
                        cadhtml += "    <td class=\"tfrom\">" + couponv.From + "<\/td>";
                        cadhtml += "    <td class=\"tto\">" + couponv.To + "<\/td>";
                        cadhtml += "    <td class=\"tcreated\">-<\/td>";
                        cadhtml += "    <td class=\"tdatecreated\">-<\/td>";
                        cadhtml += "    <td class=\"tstatus\">" + couponv.Statusname + "<\/td>";
                        cadhtml += "<\/tr>";

                        couponsresult += cadhtml;
                    }

                    $("#tabla-coupon").html(couponsresult);

                    console.log(result);
                    pageLoading(false);

                }
            });

        }
    });

    $('.pagination-code').pagination({
        items: $('#total-code').val(),
        itemsOnPage: 10,
        cssStyle: 'light-theme',
        onPageClick: function (pageNumber, event) {
            event.preventDefault();

            $("#tabla-code").html('');
            pageLoading(true);
            var formData = { idPagination: $('#idpagination-code').val(), page: pageNumber };
            var cadhtml = "";
            var coderesult = "";

            $.ajax({
                type: 'POST',
                url: '@Url.Action("PaginationAjax", "Code")',
                data: formData,
                success: function (result) {

                    coderesult = "";

                    for (var i = 0; i < result.Codes.length; i++) {

                        var codev = result.Codes[i];

                        cadhtml = "";
                        cadhtml += "<tr>";
                        cadhtml += "    <td><input type=\"checkbox\" data-id=\"" + codev.Id + "\" class=\"select-code\" \/><\/td>";
                        cadhtml += "    <td class=\"tcode\">" + codev.Code + "<\/td>";
                        cadhtml += "    <td class=\"tamount\">" + codev.Amount + "<\/td>";
                        cadhtml += "    <td class=\"tfrom\">" + codev.From + "<\/td>";
                        cadhtml += "    <td class=\"tto\">" + codev.From + "<\/td>";
                        cadhtml += "    <td class=\"tquantity\">" + codev.Quantity + "<\/td>";
                        cadhtml += "    <td class=\"t\">-<\/td>";
                        cadhtml += "    <td class=\"t\">-<\/td>";
                        cadhtml += "    <td class=\"t\">-<\/td>";
                        cadhtml += "    <td class=\"tstatus\">" + codev.Statusname + "<\/td>";
                        cadhtml += "<\/tr>";

                        coderesult += cadhtml;
                    }

                    $("#tabla-coupon").html(coderesult);

                    console.log(result);
                    pageLoading(false);

                }
            });

        }
    });
});
function deleteCoupon(formData, element) {
    $.ajax({
        type: 'POST',
        url: '/Coupon/DeleteCoupon',
        data: formData,
        success: function (data) {
            alert("Cupón eliminado correctamente");
            element.closest("tr").find(".tstatus").text("Inactivo");
        }, error: function (error) {
            alert("Error al eliminar cupón, intentelo nuevamente");
        }, complete: function (data) {
            console.log(data);
        }
    });
}
function deleteCode(formData, element) {
    $.ajax({
        type: 'POST',
        url: '/Code/DeleteCode',
        data: formData,
        success: function (data) {
            alert("Código eliminado correctamente");
            element.closest("tr").find(".tstatus").text("Inactivo");
        }, error: function (error) {
            alert("Error al eliminar código, intentelo nuevamente");
        }, complete: function (data) {
            console.log(data);
        }
    });
}
