<template>
  <div class="main-container">
    <div name="header" class="header">
      <div class="header-container">
        <div class="header-title">{{ currentTitle }}</div>
        <div class="header-title" @click="togleImportFile">Đóng form</div>
      </div>
    </div>
    <div name="sidebar" class="sidebar">
      <div class="sidebar-container">
        <div class="sidebar-elements">
          <div
            class="sidebar-option"
            :class="{ 'sidebar-option-target': isShowInputFile }"
            @click="changeView(1)"
          >
            <div class="sidebar-option-text">1.Chọn tệp nguồn</div>
          </div>
          <div
            class="sidebar-option"
            :class="{ 'sidebar-option-target': isShowtable }"
            @click="changeView(2)"
          >
            <div class="sidebar-option-text">2. Kiểm tra dữ liệu</div>
          </div>
          <div
            class="sidebar-option"
            :class="{ 'sidebar-option-target': isShowImportResult }"
            @click="changeView(3)"
          >
            <div class="sidebar-option-text">3. Kết quả nhập khẩu</div>
          </div>
        </div>
      </div>
    </div>

    <div name="main" class="main">
      <div v-if="isShowInputFile">
        <div class="file-input-container">
          <p>Chọn dữ liệu nhân viên đã chuẩn bị để nhập khẩu vào phần mềm</p>
          <input
            ref="fileInput"
            type="file"
            style="display: none"
            @change="handleFileChange"
          />
          <input
            class="input-file-name"
            type="text"
            readonly
            :value="displayFileName"
          />

          <m-button text="Chọn tệp" type="secondary" @click="openFileInput" />
          <p>
            Chưa có tệp mẫu để chuẩn bị dữ liệu? Tải tệp mẫu mà phần mềm cung
            cấp để chuẩn bị dữ liệu nhập khẩu
            <span class="blueClicks" @click="getSampleExcelFile()"
              >Tại đây</span
            >
          </p>
        </div>
      </div>

      <div v-if="isShowtable" class="view-table-container">
        <p class="notification-top">
          <span class="notification-top-left"
            >{{ this.successNumber }}/{{ this.employees.length }} dòng hợp
            lệ</span
          >
          <span
            >{{ this.failedNumer }}/{{ this.employees.length }} dòng không hợp
            lệ</span
          >
        </p>

        <div class="table-container">
          <table border="1" class="data-table">
            <thead>
              <tr>
                <th>Mã Nhân viên</th>
                <th>Tên nhân viên</th>
                <th>Giới tính</th>
                <th>Ngày sinh</th>
                <th>Chức danh</th>
                <th>Tên đơn vị</th>
                <th>Số tài khoản</th>
                <th>Tên ngân hàng</th>
                <th>Trạng thái</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="(employee, index) in this.employees" :key="index">
                <td class="table-code">{{ employee.EmployeeCode }}</td>
                <td class="table-name">{{ employee.EmployeeName }}</td>
                <td class="table-card-code">
                  {{
                    employee.Gender === 0
                      ? "Nam"
                      : employee.Gender === 1
                      ? "Nữ"
                      : "khác"
                  }}
                </td>
                <td class="table-group">
                  {{ formatDateString(employee.BirthDate) }}
                </td>
                <td class="table-phone">{{ employee.PositionName }}</td>
                <td class="table-birth">
                  {{ employee.DepartmentName }}
                </td>
                <td class="table-company">{{ employee.BankAccount }}</td>
                <td class="table-company">{{ employee.BankName }}</td>
                <td class="table-status">
                  <p v-if="employee.ErrorList.length === 0">Hợp lệ</p>
                  <p
                    class="err-status"
                    v-for="(err, index) in employee.ErrorList"
                    :key="index"
                  >
                    {{ err }}
                  </p>
                </td>
              </tr>
            </tbody>
          </table>
        </div>

        <div class="text-donwload-file">
          <p v-if="failedNumer > 0">
            Tải về tập tin chứa các dòng nhập khẩu không thành công
            <span class="blueClicks" @click="getEmployeeExcelFileBaseKey(2)"
              >Tại đây</span
            >
          </p>
        </div>
      </div>
      <div v-if="isShowImportResult">
        <h2>Kết quả nhập khẩu</h2>
        <p v-if="failedNumer > 0">
          Tải về tập tin chứa kết quả nhập khẩu
          <span class="blueClicks" @click="getEmployeeExcelFileBaseKey(1)"
            >Tại đây</span
          >
        </p>
        <p class="result-bumber">
          + Số dòng nhập khẩu thành công: {{ this.successNumber }}
        </p>
        <p class="result-bumber">
          + Số dòng nhập khẩu không thành công: {{ this.failedNumer }}
        </p>
      </div>
    </div>

    <div name="footer" class="footer-container footer">
      <m-button
        text="Giúp"
        type="icon"
        direction="left"
        icon="fa-solid fa-circle-question"
      />
      <div class="footer-btn-right">
        <m-button
          text="Quay lại"
          type="icon"
          direction="left"
          icon="fa-solid fa-left-long"
          :isdisable="isShowInputFile"
          v-if="!isShowImportResult"
          class="right-space-btn"
          @click="changeView(1)"
        />
        <m-button
          text="Tiếp tục"
          type="icon"
          direction="right"
          icon="fa-solid fa-right-long"
          class="right-space-btn"
          v-if="isShowInputFile"
          @click="changeView(2)"
        />

        <m-button
          title="Tiến hành nhập khẩu"
          text="Thực hiện"
          type="icon"
          direction="left"
          icon="fa-solid fa-arrow-right-from-bracket"
          class="right-space-btn"
          :isdisable="successNumber === 0"
          v-if="isShowtable"
          @click="importEmployees"
        />

        <m-button
          text="Hủy bỏ"
          type="icon"
          direction="left"
          icon="fa-solid fa-ban "
          v-if="!isShowImportResult"
          :isDanger="true"
          @click="togleImportFile"
        />
        <m-button
          text="Đóng"
          type="icon"
          direction="left"
          icon="fa-solid fa-power-off"
          v-if="isShowImportResult"
          :isDanger="true"
          @click="togleImportFile"
        />
      </div>
    </div>
  </div>

  <m-dialog
    v-if="isShowDialog"
    :msgs="dialogMsgs"
    :dialogType="dialogType"
    :togleDialog="togleDialog"
    :dialogIcon="dialogIcon"
  />
</template>

<script>
import MButton from "../../../base/MButton.vue";
import MDialog from "@/components/base/MDialog.vue";

import MResource from "../../../js/StringResource";
import { apiHandle } from "../../../js/ApiHandle";
import { apiFileHandle } from "../../../js/ApiHandle";

export default {
  name: "EmployeeImportFile",
  components: {
    MButton,
    MDialog,
  },
  props: {
    togleImportFile: {
      type: Function,
    },
  },
  data() {
    return {
      isShowInputFile: true,
      isShowtable: false,
      isShowImportResult: false,
      // import file
      selectedFile: null,
      // response employee from api
      employees: [],
      currentTitle: MResource.EmployoeeFileTitle.ResourceFile,
      failedNumer: 0,
      successNumber: 0,
      isImportSuccess: false,
      // for dialog
      isShowDialog: false,
      dialogMsgs: [],
      dialogType: "",
      dialogIcon: "",
      validToImportCacheKey: "",
      invalidEmployeesCacheKey: "",
      // original key include success employee and failed employee
      employeeImportCacheKey: "",
    };
  },
  methods: {
    /**
     * change view base button that user just click
     * @param {*} viewOption - view that user want to change
     * 1 - choose resource file
     * 2 - show data table
     * 3 - show import result
     * 	created by: Nguyễn Thiện Thắng
     *  created at: 2024/1/28
     */
    changeView(viewOption) {
      if (this.isFileValid()) {
        switch (viewOption) {
          case 1:
            this.isShowtable = false;
            this.isShowImportResult = false;
            this.isShowInputFile = true;
            this.currentTitle = MResource.EmployoeeFileTitle.ResourceFile;
            break;
          case 2:
            this.isShowImportResult = false;
            this.isShowInputFile = false;
            this.isShowtable = true;
            this.currentTitle = MResource.EmployoeeFileTitle.DataCheck;
            break;
          case 3:
            // not imported yet
            if (!this.isImportSuccess) {
              this.emitter.emit(
                MResource.Event.TogleDialog,
                [MResource.DialogMsg.employeeExcelFileMustBeImport],
                MResource.DialogType.warning,
                MResource.DialogIcon.warning
              );
            } else {
              this.isShowtable = false;
              this.isShowImportResult = true;
              this.isShowInputFile = false;
              this.currentTitle = MResource.EmployoeeFileTitle.ImportResult;
            }
            break;
          default:
            break;
        }
      }
    },

    /**
     * check is file valid before change view
     * 	created by: Nguyễn Thiện Thắng
     *  created at: 2024/3/19
     */
    isFileValid() {
      // file invalid
      if (this.selectedFile == null) {
        this.emitter.emit(
          MResource.Event.TogleDialog,
          [MResource.DialogMsg.employeeExcelFileMustBeValid],
          MResource.DialogType.warning,
          MResource.DialogIcon.warning
        );
        return false;
      }
      return true;
    },
    /**
     * Open file browser by custom button
     * 	created by: Nguyễn Thiện Thắng
     *  created at: 2024/1/28
     */
    openFileInput() {
      this.$refs.fileInput.click();
    },
    /**
     * assign user's filt to this.selectedFile
     * @param {*} event - user change file choosed
     * 	created by: Nguyễn Thiện Thắng
     *  created at: 2024/1/28
     */
    handleFileChange(event) {
      const selectedFile = event.target.files[0];
      this.selectedFile = selectedFile;
      this.employees = [];
      this.successNumber = 0;
      this.failedNumer = 0;
      this.isImportSuccess = false;
      this.previewEmployees();
    },
    /**
     * post file to api and read file when user want to preview
     * 	created by: Nguyễn Thiện Thắng
     *  created at: 2024/1/28
     */
    async previewEmployees() {
      this.employees = [];
      this.successNumber = 0;
      this.failedNumer = 0;
      const formData = new FormData();
      formData.append("excelFile", this.selectedFile);
      const employeeResponse = await apiHandle(
        MResource.apiMethod.post,
        MResource.apiData.previewEmployees,
        formData,
        MResource.apiHeaderContentType.formData,
        this.emitter
      );
      if (employeeResponse) {
        this.employees = employeeResponse.data.Data.employeesImport;
        this.validToImportCacheKey =
          employeeResponse.data.Data.validToImportCacheKey;
        this.invalidEmployeesCacheKey =
          employeeResponse.data.Data.invalidEmployeeCacheKey;
        this.employeeImportCacheKey =
          employeeResponse.data.Data.employeeImportCacheKey;
        this.emitter.emit(
          MResource.Event.TogleToast,
          MResource.ToastStatus.success,
          employeeResponse.data.UserMsg,
          true
        );
        this.employees.forEach((employee) => {
          if (employee.ErrorList.length === 0) {
            this.successNumber++;
          } else {
            this.failedNumer++;
          }
        });
        this.changeView(2);
      } else {
        this.selectedFile = null;
      }
    },
    /**
     * Get sample employee excelFile to import
     * 	created by: Nguyễn Thiện Thắng
     *  created at: 2024/3/21
     */
    async getSampleExcelFile() {
      let token = localStorage.getItem(MResource.Token.AccessToken);
      // get file form api
      const response = await apiFileHandle(
        MResource.apiMethod.get,
        this.resource.apiData.getSampleEmployeeExcelFile,
        this.emitter,
        null,
        token
      );
      if (response?.data) {
        const blob = new Blob([response.data], {
          type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        });
        const fileName = MResource.FileName.SampleEmployeeImportExcelFile;
        // using file-save to dowload file
        this.saveAs(blob, fileName);
      }
    },

    /**
     * Get error employee file from previouse preview
     * @param {*} getType
     * 1 - get original file( include success and failed employee )
     * 2 - get Error File
     * 	created by: Nguyễn Thiện Thắng
     *  created at: 2024/3/21
     */
    async getEmployeeExcelFileBaseKey(getType) {
      let key = "";
      if (getType === 1) {
        key = this.employeeImportCacheKey;
      } else {
        key = this.invalidEmployeesCacheKey;
      }
      let token = localStorage.getItem(MResource.Token.AccessToken);
      // get file form api
      const response = await apiFileHandle(
        MResource.apiMethod.post,
        this.resource.apiData.getEmployeeImportExcelFileBaseKey,
        this.emitter,
        key,
        token
      );
      if (response?.data) {
        const blob = new Blob([response.data], {
          type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        });
        const fileName = MResource.FileName.EmployeeExcelFile;
        // using file-save to dowload file
        this.saveAs(blob, fileName);
      }
    },

    /** format date to dd/mm/yyyy
     * @param {*} inputDateString - Date to format
     * @returns - Date formated
     * 	created by: Nguyễn Thiện Thắng
     *  created at: 2024/1/20
     */
    formatDateString(inputDateString) {
      if (inputDateString !== null) {
        var dateObject = new Date(inputDateString);
        var month = dateObject.getMonth() + 1;
        month = month < 10 ? `0${month}` : month;
        return `${dateObject.getDate()}/${month}/${dateObject.getFullYear()}`;
      } else {
        return null;
      }
    },
    /**
     * post cache key to api and insert valid employee base cache key
     * 	created by: Nguyễn Thiện Thắng
     *  created at: 2024/1/28
     * */
    async importEmployees() {
      const employeeResponse = await apiHandle(
        MResource.apiMethod.post,
        MResource.apiData.importEmployees,
        this.validToImportCacheKey,
        MResource.apiHeaderContentType.applicationType,
        this.emitter
      );
      if (employeeResponse) {
        this.emitter.emit(
          MResource.Event.TogleToast,
          MResource.ToastStatus.success,
          employeeResponse.data.UserMsg,
          true
        );
        this.isImportSuccess = true;
        this.changeView(3);
      }
    },
    /**
     * togle notice dialog
     * 	created by: Nguyễn Thiện Thắng
     *  created at: 2024/1/20
     */
    togleDialog() {
      this.isShowDialog = !this.isShowDialog;
    },
  },
  computed: {
    displayFileName() {
      return this.selectedFile
        ? this.selectedFile.name
        : MResource.FileCommon.fileDonotChoosen;
    },
  },
};
</script>
    

<style scoped>
a {
  text-decoration: none;
  color: #0103ee;
}

.blueClicks {
  color: #0103ee;
}

.blueClicks:hover {
  color: #393cec;
  cursor: pointer;
}

.main-container {
  position: fixed;
  top: 0;
  left: 50%;
  transform: translateX(-50%);
  z-index: 3;
  opacity: 100%;
  pointer-events: all;
  background: rgba(255, 255, 255);
  display: grid;
  grid-template-rows: 56px 1fr 56px;
  grid-template-columns: 200px 1fr;
  grid-template-areas:
    "header header "
    "sidebar main "
    "footer footer";
  height: 100vh;
  width: 100vw;
}

.header {
  grid-area: header;
}

.sidebar {
  grid-area: sidebar;
  background-color: #f5f7f9;
}

.main {
  border: 1px solid #dedede;
  grid-area: main;
  height: calc(100vh - 56px - 56px);
  padding-left: 12px;
}

.footer {
  grid-area: footer;
}

/* header */
.header-container {
  padding: 12px;
  display: flex;
  justify-content: space-between;
  align-items: center;
  background-color: #ffffff;
}
.header-title {
  font-weight: 600;
  cursor: pointer;
}

/* input file */

.file-input-container input::placeholder {
  color: #999999;
  font-size: 14px;
  padding-left: 12px;
}

.input-file-name {
  width: 25%;
  height: 36px;
  border-radius: 4px;
  outline: none;
  border: 1px solid #dedede;
  background-color: #f5f7f9;
  box-sizing: border-box;
  margin-right: 16px;
}

.input-file-name::placeholder {
  padding-left: 12px;
}

/* sidebar */
.sidebar-option {
  height: 44px;
  border: 1px solid #dedede;
  border-radius: 4px;
  margin-bottom: 4px;
  display: flex;
  justify-content: center;
  align-items: center;
  cursor: pointer;
}
.sidebar-option-text {
  color: black;
  font-weight: 600;
}

.sidebar-option:hover {
  background-color: #add2ed;
}
.sidebar-option-target {
  background-color: #add2ed;
}

.sidebar-elements {
  background-color: #f5f7f9;
  height: 100%;
  padding: 24px 12px;
}

/* main */
.notification-top {
  font-weight: 600;
}
.notification-top-left {
  margin-right: 120px;
  margin-bottom: 12px;
}
/* Table */

.view-table-container {
  height: 100%;
}

.table-container {
  overflow: scroll;
  height: calc(100% - 90px);
  border-bottom: 1px solid #e0e0e0;
}

.data-table {
  width: 120%;
  font-size: 14px;
  border: 1px solid #e0e0e0;
  border-collapse: collapse;
  position: relative;
}

.data-table thead {
  font-weight: normal;
}

.data-table th {
  background-color: #ededed;
  position: sticky;
  top: -1px;
  z-index: 1;
}

.data-table tr {
  height: 48px;
}

.data-table tr:nth-child(even) {
  background-color: #fafafa;
}
.data-table td {
  padding: 0 16px;
}

.table-name {
  min-width: 2vw;
}

.table-code,
.table-name,
.table-card-code,
.table-group,
.table-phone,
.table-tax,
.table-email,
.table-address,
.table-note,
.table-status,
.table-birth {
  text-align: start;
  max-width: auto;
}

.table-email,
.table-note {
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
  max-width: 100px;
}

.table-status {
  white-space: normal;
  max-width: 15vw;
  word-wrap: break-word;
}

.err-status {
  color: red;
}

.text-donwload-file {
  margin: 16px;
}

/* footoer */
.footer-container {
  padding-left: 12px;
  padding-right: 4px;
  display: flex;
  justify-content: space-between;
  align-items: center;
  background-color: #ffffff;
}

.footer-btn-right {
  display: flex;
  justify-content: flex-end;
}

.right-space-btn {
  margin-right: 12px;
}
/* import result */
.result-bumber {
  margin-left: 16px;
}
</style>