<template>
  <div class="m-textfield m-space">
    <label class="m-textfield-label" for="search-field"
      >{{ label }}
      <span v-if="isRequired" class="input-required">*</span></label
    >
    <div
      class="m-textfield-combobox"
      :style="{ border: isDataNotFound ? '1px solid red' : '' }"
      :class="{
        'm-textfield-focus': isInputFocused && !isDataNotFound,
      }"
    >
      <input
        v-model="value"
        :title="isDataNotFound ? `Nhập dữ liệu hợp lệ cho phòng ban` : ''"
        @keydown="handleKeyDown"
        id="search-field"
        @focus="changeWatchStatus"
        @blur="isInputFocused = false"
        ref="inputTagRef"
      />
      <div
        class="input-btn"
        @click="showOptionList"
        :style="{ 'border-left': isDataNotFound ? '1px solid red' : 'none' }"
      >
        <i class="fa-solid fa-chevron-down"></i>
      </div>
    </div>
    <div v-if="isShowOptionList" id="result-container">
      <div
        class="result-item item-hover"
        :class="{
          'is-item-focus': indexFocus === index,
          'result-item-checked': selectedItem === result,
        }"
        v-for="(result, index) in resultArray"
        :key="index"
        :id="index"
        @click="handleClickItem(result)"
      >
        <div class="result-icon">
          <i
            :class="{
              'fa-solid fa-check': selectedItem === result,
            }"
          ></i>
        </div>
        <div class="result-text">
          <li>{{ result }}</li>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import MResource from "../js/StringResource";
const KEY_CODE = {
  ENTER: 13,
  KEY_UP: 38,
  KEY_DOWN: 40,
};
export default {
  name: "HelloWorld",
  created() {
    this.handleClickItem(this.dataOption[0]);
  },
  props: {
    label: {
      type: String,
      required: true,
    },
    dataOption: {
      type: Array,
      required: true,
    },
    valueOption: {
      type: Array,
      required: true,
    },
    isRequired: {
      type: Boolean,
    },
  },
  data() {
    return {
      // value display in input filed
      value: this.dataOption[0],
      // real value to return
      selectedItem: this.valueOption[0],
      resultArray: [],
      isDataNotFound: false,
      indexFocus: -1,
      itemFocus: this.dataOption[0],
      shouldWatch: true,
      isShowOptionList: false,
      isInputFocused: false,
    };
  },
  methods: {
    /**
     * Search all element and find element contain keyword
     * @param {*} keyword User's keyword to find
     * @returns An array's element contain keyword
     * 	created by: Nguyễn Thiện Thắng
     *  created at: 2023/12/20
     */
    filterResult(keyword) {
      // Định nghĩa hàm removePunctuation ở ngoài hàm filterResult
      function removePunctuation(str) {
        return str.replace(/[.,/#!$%^&*;:{}=\-_`~()]/g, "");
      }

      if (keyword.trim() === "") {
        return [];
      } else {
        // Loại bỏ dấu câu từ keyword
        keyword = removePunctuation(keyword);

        return this.dataOption.filter(function (brand) {
          // Loại bỏ dấu câu từ phần tử trong mảng dataOption
          let brandWithoutPunctuation = removePunctuation(brand.toLowerCase());
          return brandWithoutPunctuation.includes(keyword.toLowerCase());
        });
      }
    },
    /**
     * search if input === some value in data option to return error notification if user do not choose any item
     * 	created by: Nguyễn Thiện Thắng
     *  created at: 2023/12/20
     */
    strictSearch() {
      const index = this.dataOption.indexOf(this.value);
      if (index === -1) {
        this.isDataNotFound = true;
        this.$emit("update:value", "");
        this.focusInput();
        return MResource.Combobox.errMsg;
      } else {
        this.isDataNotFound = false;
        let indexValue = this.dataOption.indexOf(this.value);
        let validValue = this.valueOption[indexValue];
        this.$emit("update:value", validValue);
        return "";
      }
    },
    /**
     * increase / decrease index of result array ( moving between result elements )
     * @param {*} event user's key
     * 	created by: Nguyễn Thiện Thắng
     *  created at: 2023/12/20
     */
    handleKeyDown(event) {
      switch (event.keyCode) {
        case KEY_CODE.KEY_UP:
          if (this.indexFocus > 0) {
            this.indexFocus--;
          }
          break;
        case KEY_CODE.KEY_DOWN:
          if (this.indexFocus < this.resultArray.length - 1) {
            this.indexFocus++;
          }
          break;
        case KEY_CODE.ENTER:
          this.handleClickItem(this.itemFocus);
          break;
        default:
          break;
      }
      this.itemFocus = this.resultArray[this.indexFocus];
    },
    /**
     * selected item = item that user was clicked
     * @param {*} result item result user just click
     * 	created by: Nguyễn Thiện Thắng
     *  created at: 2023/12/20
     */
    handleClickItem(result) {
      this.isDataNotFound = false;
      this.isShowOptionList = false;
      this.shouldWatch = false;
      this.selectedItem = result;
      this.value = result;
      this.validateInput();
      this.resultArray = [];
    },
    /**
     * show and hide option list
     * 	created by: Nguyễn Thiện Thắng
     *  created at: 2023/12/20
     */
    showOptionList() {
      this.isShowOptionList = !this.isShowOptionList;
      if (this.isShowOptionList) {
        this.resultArray = this.dataOption;
      } else {
        this.resultArray = [];
      }
    },
    /**
     * show icon in selected item
     * @param {*} result check is result in current item equals selected item
     * 	created by: Nguyễn Thiện Thắng
     *  created at: 2023/12/20
     */
    isCheckIconVisible(result) {
      return this.selectedItem === result;
    },
    /**
     * start tracking data in input when input was focused
     * 	created by: Nguyễn Thiện Thắng
     *  created at: 2023/12/20
     */
    changeWatchStatus() {
      this.isInputFocused = true;
      this.shouldWatch = true;
    },
    /**
     * get Valid value base option that use was choosed and return for user
     * 	created by: Nguyễn Thiện Thắng
     *  created at: 2023/12/20
     */
    validateInput() {
      if (this.isDataNotFound) {
        this.$emit("update:value", "");
      }
      let indexValue = this.dataOption.indexOf(this.selectedItem);
      let validValue = this.valueOption[indexValue];
      this.$emit("update:value", validValue);
    },
    /**
     * focus into input tag
     * 	created by: Nguyễn Thiện Thắng
     *  created_at: 2024/1/20
     */
    focusInput() {
      this.$nextTick(() => {
        this.$refs.inputTagRef.focus();
      });
    },
  },

  watch: {
    value(newValue) {
      // input is focusing
      if (this.shouldWatch) {
        this.selectedItem = "";
        this.resultArray = this.filterResult(newValue);
        if (this.resultArray.length < 1) {
          this.isDataNotFound = true;
          this.isShowOptionList = false;
        } else {
          this.isShowOptionList = true;
          this.isDataNotFound = false;
        }
        this.validateInput();
      }
    },
  },
};
</script>


<style scoped>
.m-textfield {
  position: relative;
}

.m-textfield-label {
  font-weight: 600;
}

.m-textfield label {
  font-size: 14px;
}

.input-required {
  color: red;
}

/* input field for combobox */

.m-textfield-combobox {
  height: 34px;
  width: 100%;
  border-radius: 4px;
  border: 1px solid #e6e6e6;
  margin-top: 8px;
  display: flex;
  align-items: center;
}

.m-textfield-comboboxs:hover {
  background-color: #e6e6e6;
  cursor: pointer;
}

.m-textfield-combobox input {
  padding: 12px;
  width: 100%;
  font-size: 14px;
  padding-top: 0px;
  padding-bottom: 0px;
  border: none;
  height: 100%;
  outline: none;
  border-radius: 4px;
}

.m-textfield-combobox input::placeholder {
  color: #999999;
  font-size: 14px;
}

.m-textfield-combobox input:hover {
  background-color: #e6e6e6;
  cursor: pointer;
}

.input-btn {
  padding: 0 12px;
  height: 100%;
  display: flex;
  align-items: center;
  justify-content: center;
  text-align: center;
  border-left: 1px solid #e6e6e6;
}

.input-btn:hover {
  cursor: pointer;
}

/* result */

#result-container {
  position: absolute;
  /* max-width: 100%; */
  width: 100%;
  max-height: 308px;
  border-radius: 4px;
  overflow-y: scroll;
  border: 1px solid #e6e6e6;
  background-color: white;
}

/* result */
#result-container::-webkit-scrollbar {
  width: 0px;
}

#result-container::-webkit-scrollbar-thumb {
  background-color: gray;
  border-radius: 7px;
}

.result-item {
  height: 36px;
  color: black;
  display: flex;
  align-items: center;
  width: 100%;
}

.item-hover:hover {
  background-color: #e6e6e6;
}

.is-item-focus {
  background-color: #e6e6e6;
}

.result-item-checked {
  background-color: #f4f5f7;
  color: whitesmoke;
  background-color: #019160;
}

.result-icon {
  height: 36px;
  width: 30px;
  display: flex;
  align-items: center;
  justify-content: center;
}

.result-item li {
  list-style-type: none;
  padding: 0px;
  width: 100%;
}
.m-textfield-focus {
  border: 1px solid #50b83c;
}
</style>
