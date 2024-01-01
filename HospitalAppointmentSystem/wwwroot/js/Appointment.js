$(document).ready(function () {
    //sayfa y�klendi�inde il�e dropdown � devre d��� b�rak
    $("#select_district").prop("disabled", true);
    $("#select_clinic").prop("disabled", true);
    $("#select_doctor").prop("disabled", true);

    //�ehir dropdown �n�n de�i�iklik olay�n� ele al
    $("#select_province").change(function () {
        var selectedProvinceId = $(this).val();

        // �l se�ilmi�se il�e dropdown'�n� aktifle�tir
        if (selectedProvinceId !== "NOSELECTED") {
            $("#select_district").prop("disabled", false);
        } else {
            // �l se�ilmemi�se il�e dropdown'�n� devre d��� b�rak
            $("#select_district").prop("disabled", true);
        }

        // Se�ilen ile g�re il�eleri getir
        $.get("/Apointment/GetDistricts", { id_province: selectedProvinceId }, function (data) {
            // Mevcut il�e se�eneklerini temizle
            $("#select_district").empty();

            // Yeni il�e se�eneklerini ekle
            $("#select_district").append('<option value="NOSELECTED">---�l�e Se�iniz---</option>');
            $.each(data, function (index, item) {
                $("#select_district").append('<option value="' + item.value + '">' + item.text + '</option>');
            });
        });
    });

    //il�e se�ilmeden majordepartment se�emez
    $("#select_district").change(function () {
        var selectedDistrictId = $(this).val();

        // �l�e se�ilmi�se klinik dropdown'�n� aktifle�tir
        if (selectedDistrictId !== "NOSELECTED") {
            $("#select_major").prop("disabled", false);
        } else {
            // �l se�ilmemi�se il�e dropdown'�n� devre d��� b�rak
            $("#select_major").prop("disabled", true);
        }
    });

    //il�e se�ilmeden hastane dropdown'u aktif olmas�n
    $("#select_hospital").prop("disabled", true);

    $("#select_major").change(function () {
        var selectedDistrictId = $("#select_district").val();
        var selectedMajorId = $("#select_major").val();


        // il�e ve klinik se�ilmi�se hastane dropdown'�n� aktifle�tir
        if (selectedDistrictId !== "NOSELECTED" && selectedMajorId !== "NOSELECTED") {

            $("#select_hospital").prop("disabled", false);
        } else {
            // il�e veya klinik se�ilmemi�se hastane dropdown'�n� devre d��� b�rak
            $("#select_hospital").prop("disabled", true);
        }

        // Se�ilen il�e ve kliniklere g�re hastaneleri getiren AJAX iste�i yap
        $.get("/Apointment/GetHospitalsForMajorDepart", { id_district: selectedDistrictId, id_majorDepart: selectedMajorId }, function (data) {
            // Mevcut hastane se�eneklerini temizle
            $("#select_hospital").empty();

            // Yeni hastane se�eneklerini ekle
            $("#select_hospital").append('<option value="NOSELECTED">---Hastane Se�iniz---</option>');
            $.each(data, function (index, item) {
                $("#select_hospital").append('<option value="' + item.value + '">' + item.text + '</option>');
            });
        });
    });


    // Hastane veya major departman se�ildi�inde poliklinikleri g�ncelle
    $("#select_hospital").change(function () {
        var selectedHospitalId = $("#select_hospital").val();
        var selectedMajorDepartId = $("#select_major").val();

        // Hastane ve major departman se�ildiyse
        if (selectedHospitalId !== "NOSELECTED" && selectedMajorDepartId !== "NOSELECTED") {
            // Poliklinik dropdown'�n� aktifle�tir
            $("#select_clinic").prop("disabled", false);

        } else {
            // Hastane veya major departman se�ilmemi�se poliklinik dropdown'�n� devre d��� b�rak
            $("#select_clinic").prop("disabled", true);
        }

        // Se�ilen hastane ve major departmana g�re poliklinikleri getir
        $.get("/Apointment/GetPoliclinicsForHospitalAndMajorDepart", { id_hospital: selectedHospitalId, id_majorDepart: selectedMajorDepartId }, function (data) {
            // Mevcut poliklinik se�eneklerini temizle
            $("#select_clinic").empty();

            // Yeni poliklinik se�eneklerini ekle
            $("#select_clinic").append('<option value="NOSELECTED">---Poliklinik Se�iniz---</option>');
            $.each(data, function (index, item) {
                $("#select_clinic").append('<option value="' + item.value + '">' + item.text + '</option>');
            });
        });
    });


    $("#select_clinic").change(function () {
        var selectedDoctorId = $(this).val();

        // �l se�ilmi�se il�e dropdown'�n� aktifle�tir
        if (selectedDoctorId !== "NOSELECTED") {
            $("#select_doctor").prop("disabled", false);
        } else {
            // �l se�ilmemi�se il�e dropdown'�n� devre d��� b�rak
            $("#select_doctor").prop("disabled", true);
        }

        // Se�ilen ile g�re il�eleri getir
        $.get("/Apointment/GetDoctors", { id_clinic: selectedDoctorId }, function (data) {
            // Mevcut il�e se�eneklerini temizle
            $("#select_doctor").empty();

            // Yeni il�e se�eneklerini ekle
            $("#select_doctor").append('<option value="NOSELECTED">---Doktor Se�iniz---</option>');
            $.each(data, function (index, item) {
                $("#select_doctor").append('<option value="' + item.value + '">' + item.text + '</option>');
            });
        });
    });


    //reset butonu
    $("#resetButton").click(function () {
        // Form i�indeki t�m input, select ve textarea'lar� temizle
        $("form")[0].reset();

        //dropdown'lar� devre d��� b�rak
        $("#select_district").prop("disabled", true);
        $("#select_clinic").prop("disabled", true);
        $("#select_major").prop("disabled", true);
        $("#select_hospital").prop("disabled", true);
        $("#select_doctor").prop("disabled", true);

    });
});