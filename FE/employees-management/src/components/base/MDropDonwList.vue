<template>
  <div>
    <label class="m-textfield-label" for="id"
      >{{ label }}
      <span v-if="isRequired" class="input-required">*</span></label
    >
    <div
      class="m-textfield-input"
      :class="{
        'm-texfield-err': errMsg !== '',
      }"
    >
      <select name="" id="">
        <option value="">Phòng nhân sự</option>
        <option value="">Phòng kế toán</option>
      </select>
    </div>
  </div>
</template>

<script>
export default {
  name: "MDropDownList",
  props: {
    regex: {
      type: RegExp,
    },
    isRequired: {
      type: Boolean,
    },
    validateMsg: {
      type: String,
    },
    type: {
      type: String,
    },
    label: {
      type: String,
      required: true,
    },
  },
  data() {
    return {
      inputValue: "",
      errMsg: "",
      errRequiredMsg: "Khong duoc de trong",
    };
  },
  methods: {
    validateInput(newValue) {
      if (this.isRequired) {
        if (newValue === "") {
          this.errMsg = this.errRequiredMsg;
          this.$emit("update:value", "");
          return false;
        } else {
          this.$emit("update:value", this.inputValue);
          this.errMsg = "";
          return true;
        }
      }
      if (this.regex) {
        if (newValue === "") {
          this.$emit("update:value", "");
          this.errMsg = this.errRequiredMsg;
          return false;
        } else if (this.regex.test(newValue)) {
          this.$emit("update:value", this.inputValue);
          this.errMsg = "";
          return true;
        } else {
          this.$emit("update:value", "");
          this.errMsg = this.validateMsg;
          return false;
        }
      }
    },
  },
};
</script>

<style scoped>
.m-textfield label {
  font-weight: 500;
  font-size: 14px;
}

.input-required {
  color: red;
}
/* ---------------------------input filed------------ */
.m-textfield-input {
  display: flex;
  align-items: center;
  height: 34px;
  margin-top: 8px;
  width: 100%;
  border-radius: 4px;
  border: 1px solid #e6e6e6;
}

.m-textfield-input select {
  height: 100%;
  width: 100%;
  padding-top: 0px;
  padding-bottom: 0px;
  border: none;
  outline: none;
  font-size: 14px;
  border-radius: 4px;
}
.m-textfield-input select option {
  height: 24px;
  width: 100%;
  border: none;
  outline: none;
  font-size: 14px;
}

.m-textfield-input input:focus {
  outline: none;
}

.m-textfield-input input:hover {
  background-color: #e6e6e6;
  cursor: pointer;
}

.m-textfield-input input::placeholder {
  color: #999999;
  font-size: 14px;
  padding-left: 12px;
}

.textfield-err {
  margin-top: 1px;
  color: red;
  height: 15px;
  background-color: white;
  font-size: 11px;
}

.m-texfield-err {
  border: 1px solid rgb(255, 49, 49);
}
</style>