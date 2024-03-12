const MResource = {

    // Toast
    ToastStatus: {
        failed: "failed",
        success: "success"
    },
    ToastContent: {
        failed: "Đã xảy ra lỗi khi tải dữ liệu",
        success: "Thao tác thành công .",
        undefinedErr: "Đã có lỗi xảy ra tại máy chủ vui lòng thử lại khi khác"
    },
    ToastIcon: {
        failed: "fa-circle-xmark",
        success: "fa-circle-check",
    },

    // Dialog
    DialogType: {
        warning: "warningDialog",
        deleteConfirm: "deleteConfirmDialog",
        error: "errDialog",
        dataChange: "dataChangeDialog",
    },

    DialogMsg: {
        confirmMultipleDelete: "Bạn có thực sự muốn xóa những nhân viên đã chọn không?",
        confirmSingleDeleteFront: "Bạn có thực sự muốn xóa Nhân viên",
        confirmSingleDeleteBack: " không?",
        confirmDataChange: "Dữ liệu đã thay đổi, bạn có muốn cất không ?",
        authorizeWarning: "Bạn không có quyền xem nội dung này",
        expiresLoginSession: "Phiên đăng nhập của bạn đã hết hạn, vui lòng đăng nhập lại."
    },

    DialogIcon: {
        warning: "warning",
        error: "not-valid",
        confirm: "confirm"
    },

    ErrorType: {
        misa: "misa",
        browser: "browser"
    },

    FileName: {
        EmployeeFile: "Danh_Sach_Nhanh_Vien.xlsx"
    },

    EmployoeeFileTitle: {
        ResourceFile: "1. Chọn tệp nguồn",
        DataCheck: "2. Kiểm tra dữ liệu",
        ImportResult: "3. Kết quả nhập khẩu"
    },

    FileCommon: {
        fileDonotChoosen: " file chưa được chọn",
    },

    Event: {
        TogleLoading: "togleLoading",
        ReloadData: "reloadData",
        TogleToast: "togleToast",
        TogleDialog: "togleDialog"
    },

    Combobox: {
        errMsg: "nhâp dữ liệu hợp lệ cho phòng ban"
    },

    Token: {
        AccessToken: "accessToken",
        RefreshToken: "refreshToken"
    },



    // API
    apiData: {
        login: "https://localhost:7250/api/Authenticate/Login",
        admin: "https://localhost:7250/api/Admin",
        getNewAccessToken: "https://localhost:7250/api/Authenticate/RefereshToken",
        getEmployees: "https://localhost:7250/api/v1/Employee",
        updateEmployee: "https://localhost:7250/api/v1/Employee",
        insertEmployee: "https://localhost:7250/api/v1/Employee",
        deleteEmployee: "https://localhost:7250/api/v1/Employee",
        getNewEmployeeCode: "https://localhost:7250/api/v1/Employee/NewEmployeeCode",
        getEmployeesPaging: "https://localhost:7250/api/v1/Employee/Paging?",
        exportEmployees: "https://localhost:7250/api/v1/Employee/ExportExcelFile?",
        previewEmployees: "https://localhost:7250/api/v1/Employee/PreviewExcelFile",
        importEmployees: "https://localhost:7250/api/v1/Employee/ImportExcelFile",
        deleteEmployeees: "https://localhost:7250/api/v1/Employee/ManyDelete",
        getTotalEmployeeRecord: "https://localhost:7250/api/v1/Employee/TotalEmployee",
        getDepartment: "https://localhost:7250/api/v1/Department",
    },
    apiMethod: {
        get: "get",
        post: "post",
        put: "put",
        delete: "delete"
    },
    apiHeaderContentType: {
        jsonType: "json",
        applicationType: "application/json",
        arrayType: "arraybuffer",
        formData: "multipart/form-data",
    }
}

export default MResource;