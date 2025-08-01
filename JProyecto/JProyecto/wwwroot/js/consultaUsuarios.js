$(function () {

  $('#TablaDatos').DataTable({
    language: {
      url: "//cdn.datatables.net/plug-ins/1.10.25/i18n/Spanish.json"
    }
  });

  $(document).on("click", ".btnUserModal", function () {

    const id = $(this).data('id');
    const rol = $(this).data('rol');
    const estado = $(this).data('estado');

    $('#IdUsuario').val(id);
    $('#rolUsuario').val(rol.toString());

    const estadoBool = estado.toString().toLowerCase() === 'true';
    $('#estadoUsuario').prop('checked', estadoBool);
  });


  $("#btnActualizarDatosUsuario").on("click", function () {

    var Autenticacion =
    {
      IdUsuario: $("#IdUsuario").val(),
      Estado: $("#estadoUsuario").is(':checked'),
      IdRol: $("#rolUsuario").val()
    }

    $.ajax({
      url: "/Usuario/ActualizarDatosUsuario",
      type: "POST",
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      data: JSON.stringify(Autenticacion),
      success: function () {
        location.reload();
      }
    });

  });

});