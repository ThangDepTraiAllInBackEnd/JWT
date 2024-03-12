<template>
  <div class="page-size-container">
    <div class="page-size-display">{{ pageSize }}</div>
    <div class="page-size-display" @click="togleOption">
      <i class="fa-solid fa-chevron-down"></i>
    </div>
    <div v-if="isShowOption" class="page-size-option">
      <div
        class="size-option"
        :class="{
          pageChecked: pageSize === 10,
        }"
        @click="changePageSize(10)"
      >
        <div class="option">10</div>
        <div class="icon">
          <i class="fa-solid fa-check" v-if="pageSize === 10"></i>
        </div>
      </div>

      <div
        class="size-option"
        :class="{
          pageChecked: pageSize === 20,
        }"
        @click="changePageSize(20)"
      >
        <div class="option">20</div>
        <div class="icon">
          <i class="fa-solid fa-check" v-if="pageSize === 20"></i>
        </div>
      </div>
      <div
        class="size-option"
        :class="{
          pageChecked: pageSize === 30,
        }"
        @click="changePageSize(30)"
      >
        <div class="option">30</div>
        <div class="icon">
          <i class="fa-solid fa-check" v-if="pageSize === 30"></i>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "MPageSize",
  data() {
    return {
      pageSize: 20,
      isShowOption: false,
    };
  },
  methods: {
    /**
     *  change page size
     * @param {*} pagesize New page size
     * 	created by: Nguyễn Thiện Thắng
     *  created_at: 2024/1/20
     */
    changePageSize(pagesize) {
      this.pageSize = pagesize;
      this.$emit("update:value", pagesize);
      this.togleOption();
    },
    togleOption() {
      this.isShowOption = !this.isShowOption;
    },
  },
};
</script>

<style scoped>
* {
  box-sizing: border-box;
}

.page-size-container {
  height: 36px;
  width: 60px;
  padding: 0 8px;
  display: flex;
  align-items: center;
  justify-content: space-between;
  position: relative;
  border: 1px solid #e0e0e0;
  border-radius: 4px;
  margin-right: 16px;
  cursor: pointer;
}
.page-size-option {
  border-radius: 4px;
  position: absolute;
  text-align: center;
  bottom: 36px;
  left: 0px;
  height: 108px;
  width: 60px;
  padding: 8px 0;
  display: flex;
  flex-direction: column;
  justify-content: space-around;
  box-shadow: rgba(100, 100, 111, 0.2) 0px 7px 29px 0px;
  background-color: white;
}
.size-option {
  width: 100%;
  height: 36px;
  display: flex;
  justify-content: space-around;
  align-items: center;
  cursor: pointer;
}

.size-option:hover {
  background-color: #c2edbe;
  color: white;
}
.option {
  text-align: left;
}

.pageChecked {
  color: #50b83c;
}
</style>