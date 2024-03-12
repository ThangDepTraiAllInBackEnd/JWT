<template>
  <div class="popup-container">
    <div class="popup-content">
      <div class="popup-header">
        <div class="header-left">
          <div class="popup-header-title">
            <h2>Thông tin nhân viên</h2>
          </div>
          <div class="popup-header-checkbox">
            <input id="isCustomer" type="checkbox" />
            <label for="isCustomer">Là khách hàng</label>
            <input id="isProvider" type="checkbox" />
            <label for="isProvider">Là nhà cung cấp</label>
          </div>
        </div>

        <div class="popup-header-icon">
          <div class="header-guidline"></div>
          <div class="header-close" @click="confirmToSaveOrNot()"></div>
        </div>
      </div>

      <div class="popup-form">
        <div class="form-row">
          <div class="form-row-left">
            <div class="row-code">
              <m-input
                label="Mã"
                :isRequired="true"
                requiredMsg="Mã không được để trống"
                ref="EmployeeCode"
                @update:value="updateValue('EmployeeCode', $event)"
              />
            </div>
            <div class="row-name">
              <m-input
                label="Tên"
                :isRequired="true"
                requiredMsg="Tên không được để trống"
                ref="EmployeeName"
                @update:value="updateValue('EmployeeName', $event)"
              />
            </div>
          </div>

          <div class="form-row-right">
            <div class="row-birth-date">
              <m-input
                label="Ngày sinh"
                type="date"
                ref="BirthDate"
                @update:value="updateValue('BirthDate', $event)"
              />
            </div>
            <div class="row-gender">
              <div class="form-gender">
                <label id="gender-label" for="">Giới tính</label>
                <div class="gender-radio">
                  <div class="gender-radio-button">
                    <label class="gender-container">
                      <input
                        class="gender-radio-value"
                        name="gender"
                        type="radio"
                        :value="0"
                        v-model="this.Employee.Gender"
                      />
                      <span class="checkmark"></span>
                    </label>
                    <label for="" class="gender-name">Nam</label>
                  </div>

                  <div class="gender-radio-button">
                    <label class="gender-container">
                      <input
                        class="gender-radio-value"
                        name="gender"
                        type="radio"
                        :value="1"
                        v-model="this.Employee.Gender"
                      />
                      <span class="checkmark"></span>
                    </label>
                    <label for="" class="gender-name">Nữ</label>
                  </div>

                  <div class="gender-radio-button">
                    <label class="gender-container">
                      <input
                        class="gender-radio-value"
                        name="gender"
                        type="radio"
                        :value="2"
                        v-model="this.Employee.Gender"
                      />
                      <span class="checkmark"></span>
                    </label>
                    <label for="" class="gender-name">Khác</label>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>

        <div class="form-row">
          <div class="form-row-left">
            <div class="row-department">
              <m-combobox
                label="Đơn vị"
                :dataOption="dataOptionDepartment"
                :valueOption="valueOptionDepartment"
                :isRequired="true"
                ref="DepartmentId"
                @update:value="updateValue('DepartmentId', $event)"
              />
            </div>
          </div>
          <div class="form-row-right">
            <div class="row-identity">
              <m-input
                label="Số CMND"
                title="Số chứng minh nhân dân"
                ref="Identity"
                @update:value="updateValue('Identity', $event)"
              />
            </div>
            <div class="row-provide-date">
              <m-input
                label="Ngày cấp"
                type="date"
                ref="ProvideDate"
                @update:value="updateValue('ProvideDate', $event)"
              />
            </div>
          </div>
        </div>

        <div class="form-row">
          <div class="form-row-left">
            <div class="row-position">
              <m-input
                label="Chức danh"
                ref="PositionName"
                @update:value="updateValue('PositionName', $event)"
              />
            </div>
          </div>
          <div class="form-row-right">
            <div class="row-provide-place">
              <m-input
                label="Nơi cấp"
                ref="ProvidePlace"
                @update:value="updateValue('ProvidePlace', $event)"
              />
            </div>
          </div>
        </div>

        <div class="row-address">
          <m-input
            label="Địa chỉ"
            ref="Address"
            @update:value="updateValue('Address', $event)"
          />
        </div>

        <div class="form-row">
          <div class="row-mobile-phone">
            <m-input
              label="ĐT di động"
              ref="MobilePhone"
              @update:value="updateValue('MobilePhone', $event)"
            />
          </div>
          <div class="row-landline">
            <m-input
              label="ĐT cố định"
              ref="LandLine"
              @update:value="updateValue('LandLine', $event)"
            />
          </div>
          <div class="row-email">
            <m-input
              label="Email"
              ref="Email"
              @update:value="updateValue('Email', $event)"
            />
          </div>
        </div>

        <div class="form-row">
          <div class="row-mobile-phone">
            <m-input
              label="Tài khoản ngân hàng"
              ref="BankAccount"
              @update:value="updateValue('BankAccount', $event)"
            />
          </div>
          <div class="row-landline">
            <m-input
              label="Tên Ngân hàng"
              ref="BankName"
              @update:value="updateValue('BankName', $event)"
            />
          </div>
          <div class="row-email">
            <m-input
              label="Chi nhánh"
              ref="Branch"
              @update:value="updateValue('Branch', $event)"
            />
          </div>
        </div>

        <div class="form-footer">
          <div class="cancel-btn">
            <m-button type="secondary" text="Hủy" @click="toglePopup" />
          </div>
          <div class="action-btn">
            <m-button
              type="secondary"
              text="Cất"
              class="left-btn"
              @click="beforeSubmit(1)"
            />
            <m-button
              type="primary"
              text="Cất và Thêm"
              @click="beforeSubmit(2)"
            />
          </div>
        </div>
      </div>
    </div>
  </div>
  <m-dialog
    v-if="isShowDialog"
    :msgs="dialogMsgs"
    :dialogType="dialogType"
    :togleDialog="togleDialog"
    :dialogIcon="dialogIcon"
    @confirmSave="beforeSubmit(1)"
    @confirmNotSave="toglePopup"
  />
</template>

<script>
import MInput from "../../../base/MTextField.vue";
import MButton from "@/components/base/MButton.vue";
import MCombobox from "@/components/base/MCombobox.vue";
import MDialog from "@/components/base/MDialog.vue";

import MResource from "../../../js/StringResource";
import { apiHandle } from "../../../js/ApiHandle";
import Enum from "../../../js/Enum";

export default {
  components: { MInput, MButton, MCombobox, MDialog },
  name: "EmployeePopUp",
  created() {
    this.$nextTick(() => {
      this.$refs.EmployeeCode && this.$refs.EmployeeCode.focusInput();
    });
    if (this.employeeParameter !== null) {
      delete this.employeeParameter.isChecked;
      delete this.employeeParameter.isShowAction;
    }
    // set data to form ( 1-create,  2-update, 3-replicate) base form mode
    this.setDataBaseFormMode(this.currentFormMode);
  },
  props: {
    toglePopup: {
      type: Function,
      required: true,
    },
    employeeParameter: {
      type: Object,
    },
    formMode: {
      type: Number,
    },
    departments: {
      type: Array,
    },
  },
  data() {
    return {
      Employee: {
        Gender: 0,
      },
      // Employee: employeeParameter,
      newEmployeeCode: "",
      dataOptionDepartment: this.departments.map((d) => d.DepartmentName),
      valueOptionDepartment: this.departments.map((d) => d.DepartmentId),
      // for dialog
      isShowDialog: false,
      dialogMsgs: [],
      dialogType: "",
      dialogIcon: "",
      // 1="close form" - 2="close and reset form to continue create new employee"
      submitType: 0,
      currentFormMode: this.formMode,
      // to check should focus in error filed or not
      isValidating: false,
    };
  },
  methods: {
    /**
     * togle notice dialog
     * 	created by: Nguyễn Thiện Thắng
     *  created at: 2024/1/20
     */
    togleDialog() {
      if (this.isValidating) {
        this.$refs.DepartmentId.strictSearch();
        this.$refs.EmployeeName.validateInput(this.Employee.EmployeeName);
        this.$refs.EmployeeCode.validateInput(this.Employee.EmployeeCode);
      }
      this.isShowDialog = !this.isShowDialog;
    },
    /**
     * update value for Employee from input component if value valid
     * @param {*} type Employee Propery
     * @param {*} newValue new value updated form input component
     * 	created by: Nguyễn Thiện Thắng
     *  created at: 2024/1/20
     */
    updateValue(type, newValue) {
      this.Employee[type] = newValue;
    },
    /**
     * get new employee code from api
     * 	created by: Nguyễn Thiện Thắng
     *  created at: 2024/1/20
     */
    async getNewEmployeeCode() {
      const newEmployeeCode = await apiHandle(
        MResource.apiMethod.get,
        this.resource.apiData.getNewEmployeeCode,
        null,
        null,
        this.emitter
      );
      if (newEmployeeCode) {
        return newEmployeeCode.data.Data;
      }
    },
    /**
     * set data for all employee's properties base form mode
     * @param {*} formMode - create | update | duplicate
     * 	created by: Nguyễn Thiện Thắng
     *  created at: 2024/1/20
     */

    async setDataBaseFormMode(formMode) {
      switch (formMode) {
        // create mode
        case 1:
          this.newEmployeeCode = await this.getNewEmployeeCode();
          this.$nextTick(() => {
            // new employee code valid-> assign to employee
            if (!this.newEmployeeCode) {
              this.newEmployeeCode = "";
            }
            if (
              this.$refs.EmployeeCode.validateInput(this.newEmployeeCode) === ""
            ) {
              this.Employee.EmployeeCode = this.newEmployeeCode;
            }
          });
          break;
        // update mode
        case 2:
          this.$nextTick(() => {
            this.mapParameterToEmployee();
          });
          break;
        // replicate:
        case 3:
          this.Employee = Object.assign({}, this.employeeParameter);
          this.$nextTick(() => {
            this.mapParameterToEmployee();
          });
          this.newEmployeeCode = await this.getNewEmployeeCode();
          this.$nextTick(() => {
            // new employee code valid-> assign to employee
            if (this.newEmployeeCode === undefined) {
              this.newEmployeeCode = "";
            }
            if (
              this.$refs.EmployeeCode.validateInput(this.newEmployeeCode) === ""
            ) {
              this.Employee.EmployeeCode = this.newEmployeeCode;
            }
            delete this.Employee.EmployeeId;
          });
          break;
        default:
          break;
      }
    },
    /**
     * compare two obj to check all prop and props'value are equal each other or not
     * @param {*} obj1 -object to compare
     * @param {*} obj2 -object to compare
     * 	created by: Nguyễn Thiện Thắng
     *  created at: 2024/1/20
     */
    compareObjects(obj1, obj2) {
      // Loop through all properties in obj1
      for (let key in obj1) {
        // Check if obj2 also has the property
        if (Object.prototype.hasOwnProperty.call(obj2, key)) {
          // If the values are not equal, return false
          if (obj1[key] !== obj2[key]) {
            return false;
          }
        } else {
          // If obj2 does not have the property, return false
          return false;
        }
      }
      for (let key in obj2) {
        // Check if obj1 also has the property
        if (!Object.prototype.hasOwnProperty.call(obj1, key)) {
          // If obj1 does not have the property, return false
          return false;
        }
      }
      //  all properties are equal -> return true
      return true;
    },
    /**
     *  confirm save data in from if user click close button and form's data was change compare to default data
     * 	created by: Nguyễn Thiện Thắng
     *  created at: 2024/1/20
     */
    confirmToSaveOrNot() {
      // when click close btn while form is create form
      if (this.currentFormMode === Enum.FormMode.CREATE) {
        // create a temp employee to avoid affect to this.Employee
        const tempEmployee = this.Employee;
        const hasAnyValue = Object.keys(tempEmployee)
          .filter(
            (key) =>
              key !== "EmployeeCode" &&
              key !== "Gender" &&
              key !== "DepartmentId" &&
              key !== "DepartmentName"
          )
          .some(
            (key) => tempEmployee[key] !== null && tempEmployee[key] !== ""
          );
        // at least one prop has value -> user have to confirm save data or not before close form
        if (
          hasAnyValue ||
          // department diffirent default department
          this.Employee.DepartmentId !== this.valueOptionDepartment[0] ||
          this.Employee.Gender !== 0
        ) {
          this.dialogMsgs = [MResource.DialogMsg.confirmDataChange];
          this.dialogType = MResource.DialogType.dataChange;
          this.dialogIcon = MResource.DialogIcon.confirm;
          this.isShowDialog = !this.isShowDialog;
        } else {
          this.toglePopup();
        }
      } else if (this.currentFormMode === Enum.FormMode.UPDATE) {
        // to change employee parameter prop because change employeeParameter's prop here is complicated
        let employeeParameterClone = Object.assign({}, this.employeeParameter);
        // after open update form, user was change some information -> confirm save or not
        if (
          this.compareObjects(this.Employee, employeeParameterClone) == false
        ) {
          this.dialogMsgs = [MResource.DialogMsg.confirmDataChange];
          this.dialogType = MResource.DialogType.dataChange;
          this.dialogIcon = MResource.DialogIcon.confirm;
          this.isShowDialog = !this.isShowDialog;
        } else {
          this.toglePopup(0);
        }
      } else {
        this.toglePopup();
      }
    },
    /**
     * submit from base form mode
     * @param {*} submitType to set submit tupe and using right api method
     * 	created by: Nguyễn Thiện Thắng
     *  created at: 2024/1/20
     */
    beforeSubmit(submitType) {
      this.submitType = submitType;
      if (this.isAllDataValid()) {
        this.submitForm();
      }
    },
    /**
     * Check is all employee props valid before submit
     * 	created by: Nguyễn Thiện Thắng
     *  created at: 2024/1/20
     */
    isAllDataValid() {
      let departmentMsg = this.$refs.DepartmentId.strictSearch();
      let codeMsg = this.$refs.EmployeeCode.validateInput(
        this.Employee.EmployeeCode
      );

      let nameMsg = this.$refs.EmployeeName.validateInput(
        this.Employee.EmployeeName
      );
      // all required data is valid
      if (codeMsg === "" && nameMsg === "" && departmentMsg === "") {
        return true;
      }
      this.isValidating = true;
      this.dialogMsgs = [];
      this.dialogMsgs.push(codeMsg);
      this.dialogMsgs.push(nameMsg);
      this.dialogMsgs.push(departmentMsg);
      this.dialogType = MResource.DialogType.error;
      this.dialogIcon = MResource.DialogIcon.error;
      this.isShowDialog = !this.isShowDialog;
      return false;
    },
    /**
     * submit form base form mode
     * 	created by: Nguyễn Thiện Thắng
     *  created at: 2024/1/20
     */
    async submitForm() {
      // using post when user create  || replicate
      if (
        this.currentFormMode === Enum.FormMode.CREATE ||
        this.currentFormMode === Enum.FormMode.REPLICATION
      ) {
        const employeeResponse = await apiHandle(
          MResource.apiMethod.post,
          this.resource.apiData.insertEmployee,
          this.Employee,
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
          this.emitter.emit(MResource.Event.ReloadData);
        }
        // using put method to update
      } else if (this.currentFormMode === Enum.FormMode.UPDATE) {
        const employeeResponse = await apiHandle(
          MResource.apiMethod.put,
          this.resource.apiData.updateEmployee,
          this.Employee,
          null,
          this.emitter
        );

        if (employeeResponse) {
          this.emitter.emit(
            MResource.Event.TogleToast,
            MResource.ToastStatus.success,
            employeeResponse.data.UserMsg,
            true
          );
        }
      }
      // click to " Cất " button -> close Form
      if (this.submitType === 1) {
        this.toglePopup();
        // click to "Cất và Thêm Mới" button -> reset Form and EmployeeValue
      } else {
        // the second time submit always create form
        this.currentFormMode = Enum.FormMode.CREATE;
        this.resetForm();
      }
      this.emitter.emit(MResource.Event.ReloadData);
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
        return "";
      }
    },
    /**
     * format date to yyyy/mm/dd to apply into update form
     * @param {*} inputDateString - Date to format to update in update form
     * @returns - Date formated
     * 	created by: Nguyễn Thiện Thắng
     *  created at: 2024/1/20
     */
    formatDateToUpdate(inputDateString) {
      if (inputDateString == null) {
        return null;
      } else {
        var inputDate = new Date(inputDateString);
        var year = inputDate.getFullYear();
        var month = (inputDate.getMonth() + 1).toString().padStart(2, "0");
        var day = inputDate.getDate().toString().padStart(2, "0");
        return `${year}-${month}-${day}`;
      }
    },
    /**
     * get department name by employee
     * @param {*} employee - to get department name
     * @returns - Department name
     * 	created by: Nguyễn Thiện Thắng
     *  created at: 2024/1/20
     */
    getDepartmentName(employee) {
      const department = this.departments.find(
        (de) => de.DepartmentId === employee.DepartmentId
      );
      return department ? department.DepartmentName : "";
    },
    /**
     * reset form and get new code
     * 	created by: Nguyễn Thiện Thắng
     *  created at: 2024/1/20
     */
    resetForm() {
      var attributesToRemove = [
        "EmployeeId",
        "EmployeeCode",
        "Gender",
        "DepartmentId",
        "DepartmentName",
        "ModifiedBy",
        "ModifiedDate",
        "CreatedDate",
        "CreatedBy",
      ];
      let employeeClone = Object.assign({}, this.Employee);
      const remainingAttributes = this.getRemainingAttributes(
        employeeClone,
        attributesToRemove
      );
      remainingAttributes.forEach((refName) => {
        if (this.$refs[refName]) {
          this.$refs[refName].setDataFromParent("");
        }
      });
      this.$refs.DepartmentId.handleClickItem(this.dataOptionDepartment[0]);
      this.Employee.Gender = 0;
      this.setDataBaseFormMode(Enum.FormMode.CREATE);
    },
    /**
     * Get all prop names in the object except some props
     * @param {*} obj - to get prop name
     * @param {*} attributesToRemove - ignore prop name
     * @returns - prop name array
     * 	created by: Nguyễn Thiện Thắng
     *  created at: 2024/3/6
     */
    getRemainingAttributes(obj, attributesToRemove) {
      for (let attribute of attributesToRemove) {
        delete obj[attribute];
      }
      return Object.keys(obj);
    },

    /**
     * Map all data from employeeParameter to this.Employee and display to text field
     * 	created by: Nguyễn Thiện Thắng
     *  created at: 2024/1/20
     */
    mapParameterToEmployee() {
      this.Employee = Object.assign({}, this.employeeParameter);
      this.$refs.ProvideDate.validateInput(
        this.formatDateToUpdate(this.Employee.ProvideDate)
      );

      this.$refs.BirthDate.validateInput(
        this.formatDateToUpdate(this.Employee.BirthDate)
      );

      this.$refs.DepartmentId.handleClickItem(
        this.getDepartmentName(this.Employee)
      );

      var attributesToRemove = [
        "EmployeeId",
        "Gender",
        "DepartmentId",
        "DepartmentName",
        "ModifiedBy",
        "ModifiedDate",
        "CreatedDate",
        "CreatedBy",
      ];
      let employeeClone = Object.assign({}, this.Employee);
      const remainingAttributes = this.getRemainingAttributes(
        employeeClone,
        attributesToRemove
      );
      remainingAttributes.forEach((propName) => {
        this.$refs[propName].validateInput(employeeClone[propName]);
      });
      this.Employee = Object.assign({}, this.employeeParameter);
      console.log(this.Employee);
    },
  },
};
</script>
<style scoped>
.popup-container {
  width: 100%;
  height: 100vh;
  display: flex;
  justify-content: center;
  position: fixed;
  top: 0;
  left: 50%;
  transform: translateX(-50%);
  z-index: 4;
  opacity: 100%;
  pointer-events: all;
  background: rgb(0, 0, 0, 0.5);
}

.popup-content {
  position: absolute;
  top: 7.5%;
  width: 65vw;
  padding: 0 24px;
  height: 85%;
  border-radius: 4px;
  background-color: #ffff;
}

/* header */
.popup-header {
  width: 100%;
  display: flex;
  align-items: center;
  justify-content: space-between;
}

.popup-header-checkbox {
  margin-left: 24px;
}

.popup-header input {
  transform: scale(1.3);
  accent-color: #57c841;
  margin-right: 12px;
  cursor: pointer;
}
.popup-header label {
  margin-right: 30px;
}

.header-left {
  display: flex;
  align-items: center;
}

.popup-header-icon {
  display: flex;
  align-items: flex-end;
}

.header-guidline {
  background: url("../../../assets/img/Sprites.64af8f61.svg") no-repeat -89px -145px;
  width: 22px;
  height: 22px;
  margin-right: 12px;
  cursor: pointer;
}

.header-close {
  background: url("../../../assets/img/Sprites.64af8f61.svg") no-repeat -146px -147px;
  width: 20px;
  height: 20px;
  cursor: pointer;
}

/* end header */

/* Form */

.popup-form {
  position: relative;
  height: 100%;
}

.form-row {
  width: 100%;
  display: flex;
  margin-top: 16px;
}

.form-row-left {
  margin-right: 30px;
}

.form-row-left,
.form-row-right {
  display: flex;
  align-items: center;
  width: 50%;
}

.row-code {
  width: 40%;
  margin-right: 8px;
}

.row-name {
  width: 60%;
}
.row-birth-date {
  width: 40%;
  margin-right: 8px;
}
.row-gender {
  width: 60%;
}

.row-department {
  width: 100%;
}

.row-identity {
  width: 60%;
  margin-right: 8px;
}

.row-provide-date {
  width: 40%;
}

.row-position,
.row-provide-place,
.row-address {
  width: 100%;
}

.row-address {
  margin-top: 24px;
}

.row-mobile-phone,
.row-landline {
  margin-right: 8px;
}

.row-mobile-phone,
.row-landline,
.row-email {
  width: 25%;
}

/* radio button */
.form-gender {
  text-align: start;
}

#gender-label {
  font-weight: 600;
  font-size: 14px;
}

.gender-radio {
  display: flex;
  align-items: center;
  height: 36px;
  margin-top: 8px;
}

.gender-radio-button {
  display: flex;
  align-items: center;
  margin: 0 10px;
}

.gender-container {
  display: inline-block;
  position: relative;
  cursor: pointer;
  font-size: 20px;
  user-select: none;
  padding-left: 40px;
}

.gender-name {
  font-size: 14px;
  margin-left: -20px;
}

.gender-container input {
  opacity: 0;
  cursor: pointer;
}

.checkmark {
  position: absolute;
  top: 0;
  left: 0;
  width: 23px;
  height: 23px;
  background: #eee;
  border-radius: 50%;
}

.gender-container:hover .checkmark {
  background: #ccc;
}

.checkmark:after {
  content: "";
  position: absolute;
  display: none;
}

.gender-container .checkmark:after {
  top: 50%;
  left: 50%;
  width: 10px;
  height: 10px;
  border-radius: 50%;
  border: solid 3px white;
  transform: translate(-50%, -50%) rotate(45deg);
}

.gender-container input:checked ~ .checkmark {
  background: #57c841;
}

.gender-container input:checked ~ .checkmark:after {
  display: block;
}

/* end radio button */

.form-footer {
  width: 100%;
  height: 84px;
  display: flex;
  justify-content: space-between;
  padding: 12px 0;
  margin-top: 31px;
  border-top: 1px solid #e6e6e6;
}

.left-btn {
  margin-right: 8px;
}

/* end form */
</style>