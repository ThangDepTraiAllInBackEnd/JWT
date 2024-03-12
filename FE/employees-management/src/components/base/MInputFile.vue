<template>
  <div class="file-input-container">
    <p>Chọn dữ liệu khách hàng đã chuẩn bị để nhập khẩu vào phần mềm</p>
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

    <m-button @click="openFileInput" text="Chọn" type="secondary"
      >Chọn file</m-button
    >
    <p>
      Chưa có tệp mẫu để chuẩn bị dữ liệu? Tải têp excel mẫu mà phần mềm cung
      cấp để chuẩn bị dữ liệu nhập khẩu <span><a href="">tại đây</a></span>
    </p>
  </div>
</template>

<script>
import MResource from "../js/StringResource";
import MButton from "./MButton.vue";
export default {
  components: { MButton },
  name: "MInputFile",

  data() {
    return {
      selectedFile: null,
    };
  },
  computed: {
    displayFileName() {
      return this.selectedFile
        ? this.selectedFile.name
        : MResource.FileCommon.fileDonotChoosen;
    },
  },
  methods: {
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
    },
  },
};
</script>

<style scoped>
a {
  text-decoration: none;
  color: #0103ee;
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
</style>