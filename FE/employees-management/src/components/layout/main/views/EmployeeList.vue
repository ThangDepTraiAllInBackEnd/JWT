<template>
  <div class="main-top">
    <h2>Nhân viên</h2>
    <m-button
      text="Thêm mới nhân viên"
      type="primary"
      @click="toglePopup(FormMode.CREATE)"
    />
  </div>
  <div class="main-content">
    <div class="main-action">
      <div
        class="action-batch-operation"
        v-if="numberOfChecked > 0"
        @click="this.isShowBatchDelete = !this.isShowBatchDelete"
      >
        <div>Thực hiện hàng loạt</div>
        <div class="action-batch-operation-icon"></div>
        <div
          class="action-batch-operation-button"
          v-if="this.isShowBatchDelete"
          @click="setDeleteMode(null)"
        >
          Xóa
        </div>
      </div>
      <div class="action-tools">
        <form @submit.prevent="getEmployees(1)">
          <m-search-field
            placeholder="tìm theo mã, tên nhân viên"
            @update:value="updateSearchKey($event)"
            ref="searchRef"
          />
        </form>

        <div
          class="reload-btn"
          @click="getEmployees(this.currentPage)"
          title="Tải lại dữ liệu"
        ></div>
        <div
          class="import-btn"
          title="Nhập khẩu dữ liệu"
          @click="togleImportFile"
        ></div>
        <div
          class="export-btn"
          title="Xuất khẩu dữ liệu"
          @click="togleExportOption"
        ></div>
        <div class="export-option" v-if="isShowExportOption">
          <div
            class="export-option-element"
            @click="exportEmployeesToExcelFile(1)"
          >
            Xuất những bản ghi đang xem
          </div>
          <div
            class="export-option-element"
            @click="exportEmployeesToExcelFile(2)"
          >
            Xuất toàn bộ bản ghi nhân viên
          </div>
        </div>
      </div>
    </div>
    <div class="main-data-table">
      <table class="data-table">
        <thead>
          <tr>
            <th class="header-checkbox">
              <input
                v-model="isAllChecked"
                @change="togleAllCheckBox()"
                class="data-table-checkbox"
                type="checkbox"
                id="checkbox-control"
              />
            </th>
            <th class="header-code">Mã Nhân Viên</th>
            <th class="header-full-name">Tên Nhân Viên</th>
            <th class="header-gender">Giới Tính</th>
            <th class="header-birth-date">Ngày Sinh</th>
            <th class="header-identity">Số CMND</th>
            <th class="header-position">Chức Danh</th>
            <th class="header-department">Tên Đơn Vị</th>
            <th class="header-account-number">Số Tài Khoản</th>
            <th class="header-bank">Tên Ngân Hàng</th>
            <th class="header-branch">Chi Nhánh TK Ngân Hàng</th>
          </tr>
        </thead>
        <tbody v-if="employees !== []">
          <tr
            class="body-row"
            v-for="employee in employees"
            :key="employee.EmployeeId"
            :class="{ 'is-rowhover': employee.isChecked }"
          >
            <td class="body-checkbox">
              <input
                v-model="employee.isChecked"
                class="data-table-checkbox"
                type="checkbox"
                @click="toggleEmployeeCheckbox(employee)"
              />
            </td>
            <td class="body-code">{{ employee.EmployeeCode }}</td>
            <td class="body-full-name">{{ employee.EmployeeName }}</td>
            <td class="body-gender">
              {{
                employee.Gender === 0
                  ? "Nam"
                  : employee.Gender === 1
                  ? "Nữ"
                  : "khác"
              }}
            </td>
            <td class="body-birth-date">
              {{ formatDateString(employee.BirthDate) }}
            </td>
            <td class="body-identity">{{ employee.Identity }}</td>
            <td class="body-department">{{ employee.PositionName }}</td>
            <td class="body-department">
              {{ employee.DepartmentName }}
            </td>
            <td class="body-account-number">{{ employee.BankAccount }}</td>
            <td class="body-bank">{{ employee.BankName }}</td>
            <td class="body-branch">
              {{ employee.Branch }}
            </td>

            <div class="body-function">
              <div class="funtion-btn">
                <div
                  class="body-function-edit-btn"
                  title="Chỉnh sửa"
                  @click="openFormAndSetFormMode(FormMode.UPDATE, employee)"
                ></div>

                <div
                  class="body-function-action-btn"
                  @click="toggleEmployeeAction(employee)"
                >
                  <i class="fa-solid fa-ellipsis" style="color: #6b6c72"></i>
                </div>
                <div class="body-function-popup" v-if="employee.isShowAction">
                  <div
                    class="function-option"
                    @click="
                      openFormAndSetFormMode(FormMode.REPLICATION, employee)
                    "
                  >
                    Nhân Bản
                  </div>
                  <div class="function-option" @click="setDeleteMode(employee)">
                    Xóa
                  </div>
                </div>
              </div>
            </div>
          </tr>
        </tbody>
      </table>
    </div>
    <div class="data-table-footer">
      <div class="table-number-records">
        Tổng số: <b>{{ totalRecord }}</b> bản ghi
      </div>
      <div class="table-footer-right">
        <span class="number-per-page">Số bản ghi/trang</span>
        <m-page-size @update:value="updatePageSize($event)" />

        <span v-if="this.totalPage < 7">
          <span
            :class="{
              'disable-btn': this.currentPage === 1,
            }"
            class="back-btn"
            @click="getEmployees(this.currentPage - 1)"
            >Trước</span
          >
          <a
            v-for="index in this.totalPage"
            :key="index"
            @click="getEmployees(index)"
            :class="{ 'page-active': index === currentPage }"
            >{{ index }}</a
          >
          <span
            :class="{
              'disable-btn': this.currentPage === this.totalPage,
            }"
            class="next-btn"
            @click="getEmployees(this.currentPage + 1)"
            >Sau</span
          >
        </span>
        <span v-if="this.totalPage >= 7">
          <span
            :class="{
              'disable-btn': this.currentPage === 1,
            }"
            class="back-btn"
            @click="getEmployees(this.currentPage - 1)"
            >Trước</span
          >
          <span v-if="this.currentPage > 3">...</span>

          <span v-for="index in this.totalPage - 1" :key="index">
            <a
              @click="getEmployees(index)"
              v-if="
                index <= this.currentPage + 2 && index >= this.currentPage - 2
              "
              :class="{ 'page-active': index === currentPage }"
              >{{ index }}</a
            >
          </span>
          <span v-if="this.currentPage < this.totalPage - 3">...</span>

          <a
            @click="getEmployees(this.totalPage)"
            :class="{ 'page-active': this.totalPage === currentPage }"
            >{{ this.totalPage }}</a
          >
          <span
            :class="{
              'disable-btn': this.currentPage === this.totalPage,
            }"
            class="next-btn"
            @click="getEmployees(this.currentPage + 1)"
            >Sau</span
          >
        </span>
      </div>
    </div>
  </div>
  <employee-pop-up
    v-if="isShowPopUp"
    :toglePopup="toglePopup"
    :employeeParameter="employeeParameter"
    :formMode="currentFormMode"
    :departments="departments"
  />
  <m-dialog
    v-if="isShowDialog"
    :msgs="dialogMsgs"
    :dialogType="dialogType"
    :togleDialog="togleDialog"
    :dialogIcon="dialogIcon"
    @confirmSayYes="deleteEmployees"
  />
  <employee-import-file
    v-if="isShowImportFile"
    :togleImportFile="togleImportFile"
  />
</template>

<script>
import MDialog from "@/components/base/MDialog.vue";
import MButton from "../../../base/MButton.vue";
import MSearchField from "../../../base/MSearchField.vue";
import EmployeePopUp from "./EmployeePopUp.vue";
import EmployeeImportFile from "./EmployeeImportFile.vue";
import MPageSize from "@/components/base/MPageSize.vue";

import { apiHandle } from "../../../js/ApiHandle";
import { handleErr } from "../../../js/ApiHandle";
import { checkAuthentication } from "../../../js/TokenHandle";
import MResource from "../../../js/StringResource";
import Enum from "../../../js/Enum";

export default {
  name: "EmployeeList",
  components: {
    MButton,
    MSearchField,
    EmployeePopUp,
    MDialog,
    EmployeeImportFile,
    MPageSize,
  },
  async created() {
    await checkAuthentication(this.emitter);
    this.emitter.on(MResource.Event.ReloadData, () => {
      this.getEmployees(this.currentPage);
    });
    // get employees when create component
    this.getEmployees(this.currentPage);
    this.getDepartmentList();
  },
  data() {
    return {
      isShowPopUp: false,
      isAllChecked: false,
      employees: [],
      checkedEmployees: [],
      departments: [],
      numberOfChecked: 0,
      employeeParameter: null,
      FormMode: Enum.FormMode,
      currentFormMode: 0,
      // for dialog
      isShowDialog: false,
      dialogMsgs: [],
      dialogType: "",
      dialogIcon: "",
      // custom apiUrl
      deleteApirUrl: "",
      exportApiUrl: "",
      // for paging
      currentPage: 1,
      totalPage: 1,
      currentPageSize: 20,
      totalRecord: 0,
      searchKey: "",
      // for batch operation
      isShowbatchOperation: false,
      isShowBatchDelete: false,
      // for delete funtion -> single =1 | multiple =2
      deleteMode: 1,
      deleteData: null,
      // import file
      isShowImportFile: false,
      isShowExportOption: false,
    };
  },

  methods: {
    /**
     * pass data to form base form mode
     * @param {*} formMode - from to create || update
     * 	created by: Nguyễn Thiện Thắng
     *  created at: 2024/1/20
     */
    toglePopup(fromMode) {
      this.currentFormMode = fromMode;
      this.employeeParameter = {
        Gender: 0,
      };
      this.isShowPopUp = !this.isShowPopUp;
    },
    /**
     * togle notice/err/warning dialog
     * 	created by: Nguyễn Thiện Thắng
     *  created at: 2024/1/20
     */
    togleDialog() {
      this.isShowDialog = !this.isShowDialog;
    },
    /**
     * togle export option popup
     * 	created by: Nguyễn Thiện Thắng
     *  created at: 2024/1/20
     */
    togleExportOption() {
      this.isShowExportOption = !this.isShowExportOption;
    },
    /**
     * togle inport file dialog
     * 	created by: Nguyễn Thiện Thắng
     *  created at: 2024/1/20
     */
    togleImportFile() {
      this.isShowImportFile = !this.isShowImportFile;
    },

    /**
     * get customer from api base currentPage / page size and search key wrod
     * @param {*} currentPage - current page
     * 	created by: Nguyễn Thiện Thắng
     *  created at: 2024/1/20
     */
    async getEmployees(currentPage) {
      const employeeResponse = await apiHandle(
        MResource.apiMethod.get,
        this.resource.apiData.getEmployeesPaging +
          `page=${currentPage}&size=${this.currentPageSize}&key=${this.searchKey}`,
        null,
        null,
        this.emitter
      );
      if (employeeResponse) {
        this.isAllChecked = false;
        this.checkedEmployees = [];
        this.numberOfChecked = 0;

        // config for paging
        this.employees = employeeResponse.data.Data.records.ListRecord;
        this.currentPage = employeeResponse.data.Data.records.CurrentPage;
        this.totalPage = employeeResponse.data.Data.records.TotalPage;
        this.totalRecord = employeeResponse.data.Data.totalRecords;
        this.currentPage = currentPage;
      }
    },
    /**
     * get department list
     * 	created by: Nguyễn Thiện Thắng
     *  created at: 2024/1/20
     */
    async getDepartmentList() {
      const departmentResponse = await apiHandle(
        MResource.apiMethod.get,
        this.resource.apiData.getDepartment,
        null,
        null,
        this.emitter
      );
      if (departmentResponse) {
        this.departments = departmentResponse.data.Data;
      }
    },
    /**
     * delete employees through api base delete mode and delete data
     * 	created by: Nguyễn Thiện Thắng
     *  created at: 2024/1/20
     */
    async deleteEmployees() {
      // single delete
      if (this.deleteMode === 1) {
        this.deleteApirUrl = this.resource.apiData.deleteEmployee;
        // multiple delete
      } else if (this.deleteMode === 2) {
        this.deleteApirUrl = this.resource.apiData.deleteEmployeees;
      }

      const employeeResponse = await apiHandle(
        MResource.apiMethod.delete,
        this.deleteApirUrl,
        this.deleteData,
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
        if (this.deleteMode === 2) {
          this.checkedEmployees = [];
          this.numberOfChecked = 0;
        } else if (this.deleteMode === 1) {
          this.numberOfChecked--;
        }
        this.isAllChecked = false;
        this.updateSearchKey("");
        this.getEmployees(1);
      }
    },

    /**
     * set Delete mode(multiple || single) change delete data(array || employee) and show dialog by delete mode
     * @param {*} employee to set by delete mode: employe != null -> multiple | single
     * 	created by: Nguyễn Thiện Thắng
     *  created at: 2024/1/20
     */
    setDeleteMode(employee) {
      if (employee) {
        this.deleteMode = 1;
        this.dialogMsgs = [
          `${MResource.DialogMsg.confirmSingleDeleteFront}
           <${employee.EmployeeCode}> 
           ${MResource.DialogMsg.confirmSingleDeleteBack}`,
        ];
        this.deleteData = employee;
      } else {
        this.deleteMode = 2;
        this.dialogMsgs = [`${MResource.DialogMsg.confirmMultipleDelete}`];
        this.deleteData = this.checkedEmployees;
      }
      this.dialogType = MResource.DialogType.deleteConfirm;
      this.dialogIcon = MResource.DialogIcon.warning;
      this.isShowDialog = !this.isShowDialog;
    },

    /**
     * update new page size and get employee again
     * @param {*} newPageSize new page size to update
     * 	created by: Nguyễn Thiện Thắng
     *  created at: 2024/1/20
     */
    updatePageSize(newPageSize) {
      this.currentPageSize = newPageSize;
      this.getEmployees(1);
    },
    /**
     * update new search key and display on search filed by other component
     * @param {*} newKey new search key
     * 	created by: Nguyễn Thiện Thắng
     *  created at: 2024/1/20
     */
    updateSearchKey(newKey) {
      this.searchKey = newKey;
      this.$refs.searchRef.onUpdateFormParent(newKey);
    },
    /**
     * push employee into form for user's change form mode
     * @param {*} formMode form mode
     * @param {*} employee employee to push to form for update action
     * 	created by: Nguyễn Thiện Thắng
     *  created at: 2024/1/20
     */
    openFormAndSetFormMode(formMode, employee) {
      this.currentFormMode = formMode;
      this.employeeParameter = employee;
      this.isShowPopUp = !this.isShowPopUp;
    },
    /**
     * format date to dd/mm/yyyy
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
     * check or uncheck all check box base button that use just click
     * ( check all / uncheck all)
     * 	created by: Nguyễn Thiện Thắng
     *  created at: 2024/1/20
     */
    togleAllCheckBox() {
      this.employees.forEach((em) => {
        em.isChecked = this.isAllChecked;
      });
      // header check box is checked -> all checkbox checked
      if (this.isAllChecked) {
        this.numberOfChecked = this.employees.length;
        this.checkedEmployees = this.employees;
      } else {
        this.numberOfChecked = 0;
        this.checkedEmployees = [];
      }
    },
    /**
     * Show / hide employee action block
     * 	created by: Nguyễn Thiện Thắng
     *  created at: 2024/1/20
     */
    toggleEmployeeAction(employee) {
      employee.isShowAction = !employee.isShowAction;
    },
    /**
     * add color for row base checkbox's status
     * @param {*} employee - Checkbox in row
     * 	created by: Nguyễn Thiện Thắng
     *  created at: 2024/1/20
     */
    toggleEmployeeCheckbox(employee) {
      employee.isChecked = !employee.isChecked;
      if (employee.isChecked) {
        this.numberOfChecked++;
        this.checkedEmployees.push(employee);
      } else {
        // disable check header checkbox
        if (this.isAllChecked) {
          this.isAllChecked = false;
        }
        this.checkedEmployees = this.checkedEmployees.filter(
          (em) => em !== employee
        );
        this.numberOfChecked--;
      }
    },
    /**
     * export current employee list
     * @param {*} option - export option
     * 1 - export current records
     * 2 - export all record
     * 	created by: Nguyễn Thiện Thắng
     *  created at: 2024/1/20
     */
    async exportEmployeesToExcelFile(option) {
      await checkAuthentication(this.emitter);
      if (option === 1) {
        this.exportApiUrl = `page=${this.currentPage}&size=${this.currentPageSize}&key=${this.searchKey}`;
      } else {
        this.exportApiUrl = `page=${this.currentPage}&size=${this.totalRecord}`;
      }
      this.togleExportOption();

      try {
        let token = localStorage.getItem(MResource.Token.AccessToken);
        this.emitter.emit(MResource.Event.TogleLoading, true);
        // get file form api
        const response = await this.api.get(
          this.resource.apiData.exportEmployees + this.exportApiUrl,
          {
            responseType: MResource.apiHeaderContentType.arrayType,
            headers: {
              Authorization: `Bearer ${token}`,
            },
          }
        );

        const blob = new Blob([response.data], {
          type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        });
        const fileName = MResource.FileName.EmployeeFile;
        // using file-save to dowload file
        this.saveAs(blob, fileName);
        this.emitter.emit(MResource.Event.TogleLoading, false);
      } catch (error) {
        this.emitter.emit(MResource.Event.TogleLoading, false);
        console.log(error);
        handleErr(error, MResource.ErrorType.browser, this.emitter);
      }
    },
  },
};
</script>

<style scoped>
.main-top {
  display: flex;
  align-items: center;
  justify-content: space-between;
  width: calc(100%);
  padding-right: 12px;
  background-color: #f4f5f7;
}

.main-content {
  height: calc(100% - 67.03px);
  width: 100%;
  padding: 0 12px;
  background-color: #ffff;
  position: relative;
}

/* action */
.main-action {
  display: flex;
  align-items: center;
  padding: 12px 0;
}

.action-batch-operation {
  height: 34px;
  width: 175px;
  border-radius: 18px;
  display: flex;
  align-items: center;
  justify-content: space-evenly;
  text-align: center;
  cursor: pointer;
  font-weight: 600;
  position: relative;
  border: 1px solid black;
}

.action-batch-operation:hover,
.action-batch-operation-button:hover {
  background-color: #faf8f8;
}

.action-batch-operation-icon {
  background: url("../../../assets/img/Sprites.64af8f61.svg") no-repeat -564px -365px;
  width: 8px;
  height: 5px;
}

.action-batch-operation-button {
  height: 34px;
  width: 75px;
  position: absolute;
  top: 34px;
  left: 95px;
  z-index: 4;
  padding: 0 8px;
  text-align: start;
  display: flex;
  align-items: center;
  background-color: #ffffff;
  border: 1px solid #e0e0e0;
}

.action-tools {
  margin-left: auto;
  display: flex;
  align-items: center;
}
.reload-btn {
  background: url("../../../assets/img/Sprites.64af8f61.svg") no-repeat -1098px -90px;
  width: 20px;
  height: 21px;
  margin-left: 12px;
  cursor: pointer;
}

.import-btn {
  background: url("../../../assets/img/Sprites.64af8f61.svg") no-repeat -483px -259px;
  width: 23px;
  height: 20px;
  margin-left: 12px;
  cursor: pointer;
}

.export-btn {
  background: url("../../../assets/img/Sprites.64af8f61.svg") no-repeat -1265px -90px;
  width: 23px;
  height: 20px;
  margin-left: 12px;
  cursor: pointer;
}

.export-option {
  position: absolute;
  height: 54px;
  width: 250px;
  z-index: 4;
  right: 15px;
  top: 40px;
  opacity: 100;
  pointer-events: all;
  background-color: #ffff;
  border: 1px solid #e0e0e0;
}

.export-option-element {
  color: #999999;
  display: flex;
  align-items: center;
  text-align: start;
  padding: 0 12px;
  height: 27px;
}

.export-option-element:hover {
  color: #57c841;
  background-color: #f4f5f7;
  cursor: pointer;
}

/* end action */

/* table */
.main-data-table {
  max-height: 67vh;
  overflow: scroll;
  border-bottom: 1px solid #e0e0e0;
}

.data-table {
  width: 160%;
  font-size: 14px;
  border-radius: 10px;
  position: relative;
  border-collapse: collapse;
  color: #000;
  border: 1px solid #e0e0e0;
}

.data-table th {
  position: sticky;
  top: -1px;
  z-index: 3;
  background-color: #f4f5f7;
}

.data-table tr {
  height: 38px;
  cursor: pointer;
}

.data-table tr,
.data-table th {
  border-left: 1px solid #e0e0e0;
  border-bottom: 1px solid #e0e0e0;
  border-right: 1px solid #e0e0e0;
}

.data-table td {
  border-right: 1px solid #e0e0e0;
}

.header-code,
.header-full-name,
.header-gender,
.header-identity,
.header-position,
.header-department,
.header-account-number,
.header-bank,
.header-branch,
.body-code,
.body-full-name,
.body-gender,
.body-identity,
.body-position,
.body-department,
.body-account-number,
.body-bank,
.body-branch {
  padding: 4px 16px;
  text-align: start;
  color: #363636;
}

.body-branch {
  position: relative;
}

.header-checkbox,
.header-birth-date,
.header-function,
.body-checkbox,
.body-birth-date {
  padding: 0 16px;
  text-align: center;
  color: #363636;
}

.header-code {
  width: 9%;
}

.header-full-name {
  width: 12%;
}
.data-table-checkbox {
  transform: scale(1.7);
  outline: none;
  accent-color: #57c841;
}

.body-row {
  position: relative;
}
.body-row:hover {
  background-color: #e4f8e2;
}

.body-row:hover {
  background-color: #e4f8e2;
}

.body-row:hover .body-function {
  display: inline;
}

.body-function {
  height: 38px;
  display: flex;
  align-items: center;
  position: sticky;
  right: 0;
  z-index: 2;
  display: none;
}

.funtion-btn {
  position: absolute;
  top: 0;
  right: 0;
  display: flex;
  column-gap: 8px;
}

.body-function-edit-btn {
  text-align: center;
  width: 36px;
  height: 36px;
  border-radius: 50%;
  z-index: 1;
  pointer-events: all;
  scale: 0.8;
  background: url("../../../assets/img/Sprites.64af8f61.svg") no-repeat -1650px -83px;
  background-color: white;
  box-shadow: rgba(0, 0, 0, 0.35) 0px 5px 15px;
}

.body-function-action-btn {
  text-align: center;
  width: 36px;
  height: 36px;
  border-radius: 50%;
  z-index: 1;
  pointer-events: all;
  margin-right: 16px;
  scale: 0.8;
  font-size: 24px;
  background-color: white;
  box-shadow: rgba(0, 0, 0, 0.35) 0px 5px 15px;
}

.body-function-popup {
  position: absolute;
  height: 54px;
  width: 115px;
  z-index: 2;
  right: 15px;
  top: 25px;
  opacity: 100;
  pointer-events: all;
  /* display: none; */
  background-color: #ffff;
  border: 1px solid #e0e0e0;
}

.function-option {
  color: #999999;
  display: flex;
  align-items: center;
  text-align: start;
  padding: 0 12px;
  height: 27px;
}

.function-option:hover {
  color: #57c841;
  background-color: #f4f5f7;
}

/* footer */
.data-table-footer {
  display: flex;
  align-items: center;
  justify-content: space-between;
  position: absolute;
  bottom: 0;
  margin-bottom: 13px;
  width: calc(100% - 24px);
  background: #ffff;
}

.data-table-footer a {
  text-decoration: none;
  color: black;
  margin: 0 2px;
  padding: 0 4px;
}

.data-table-footer a:hover {
  background-color: rgb(209, 209, 209);
  text-decoration: none;
  color: black;
  margin: 0 2px;
  cursor: pointer;
}

.table-footer-right {
  display: flex;
  align-items: center;
}

.number-per-page {
  margin-right: 8px;
}
.back-btn {
  margin-right: 12px;
  cursor: pointer;
}

.next-btn {
  margin-left: 12px;
  cursor: pointer;
}

.page-active {
  border: 1px solid #e0e0e0;
  padding: 0 4px;
}

.disable-btn {
  opacity: 50%;
  pointer-events: none;
  cursor: not-allowed;
}

/* end footer */
/* end table */
</style>