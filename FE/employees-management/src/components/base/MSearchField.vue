<template>
  <div
    class="m-textfield"
    :class="{
      'm-textfield-focus': isInputFocused,
    }"
  >
    <input
      v-model="value"
      id="search-field"
      :placeholder="placeholder"
      @input="onInput"
      @focus="isInputFocused = true"
      @blur="isInputFocused = false"
    />
    <div class="input-btn"></div>
  </div>
</template>

<script>
export default {
  name: "MSearchField",
  props: {
    placeholder: {
      type: String,
      required: true,
    },
    searchKey: {
      type: String,
    },
  },
  data() {
    return {
      value: "",
      isInputFocused: false,
    };
  },
  methods: {
    onInput() {
      this.$emit("update:value", this.value);
    },
    onUpdateFormParent(value) {
      this.value = value;
    },
  },
};
</script>
<style scoped>
/* combobox */
.m-textfield {
  height: 34px;
  width: 255px;
  border-radius: 4px;
  border: 1px solid #e6e6e6;
  display: flex;
  align-items: center;
}

.m-textfields:hover {
  background-color: #e6e6e6;
  cursor: pointer;
}

.m-textfield input {
  padding: 12px;
  width: 230px;
  font-size: 14px;
  padding-top: 0px;
  padding-bottom: 0px;
  height: 100%;
  border-radius: 4px;
  border: none;
  outline: none;
}

.m-textfield input::placeholder {
  color: #999999;
  font-size: 14px;
  font-style: italic;
}

.m-textfield input:hover {
  background-color: #e6e6e6;
  cursor: pointer;
}

.input-btn {
  width: 100%;
  height: 100%;
  display: flex;
  align-items: center;
  justify-content: flex-end;
  width: 15px;
  height: 16px;
  background: url("../assets/img/Sprites.64af8f61.svg") no-repeat -33px -1664px;
}

.input-btn:hover {
  cursor: pointer;
}

.m-textfield-focus {
  border: 1px solid #50b83c;
}
</style>
