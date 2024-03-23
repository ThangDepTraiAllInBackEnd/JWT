
const baseApiUrl = "https://localhost:7250";

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
        expiresLoginSession: "Phiên đăng nhập của bạn đã hết hạn, vui lòng đăng nhập lại.",
        employeeExcelFileMustBeValid: "Vui lòng chọn 1 file đúng định dạng file mẫu để chuyển sang màn hình này.",
        employeeExcelFileMustBeImport: "Tiến hành nhập khẩu thành công để chuyển sang màn hình này."
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
        EmployeeFile: "Danh_Sach_Nhanh_Vien.xlsx",
        SampleEmployeeImportExcelFile: "SampleEmployeeImport.xlsx",
        EmployeeExcelFile: "EmployeeExcelFile.xlsx"
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
        login: `${baseApiUrl}/api/Authenticate/Login`,
        admin: `${baseApiUrl}/api/Admin`,
        getNewAccessToken: `${baseApiUrl}/api/Authenticate/RefereshToken`,
        getEmployees: `${baseApiUrl}/api/v1/Employee`,
        updateEmployee: `${baseApiUrl}/api/v1/Employee`,
        insertEmployee: `${baseApiUrl}/api/v1/Employee`,
        deleteEmployee: `${baseApiUrl}/api/v1/Employee`,
        getNewEmployeeCode: `${baseApiUrl}/api/v1/Employee/NewEmployeeCode`,
        getEmployeesPaging: `${baseApiUrl}/api/v1/Employee/Paging?`,
        exportEmployees: `${baseApiUrl}/api/v1/Employee/ExportExcelFile?`,
        previewEmployees: `${baseApiUrl}/api/v1/Employee/PreviewExcelFile`,
        importEmployees: `${baseApiUrl}/api/v1/Employee/ImportExcelFile`,
        deleteEmployeees: `${baseApiUrl}/api/v1/Employee/MultipleDelete`,
        getTotalEmployeeRecord: `${baseApiUrl}/api/v1/Employee/TotalEmployee`,
        getDepartment: `${baseApiUrl}/api/v1/Department`,
        getSampleEmployeeExcelFile: `${baseApiUrl}/api/v1/Employee/GetSampleExcelFile`,
        getEmployeeImportExcelFileBaseKey: `${baseApiUrl}/api/v1/Employee/GetEmployeeExcelFileBaseKey`
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