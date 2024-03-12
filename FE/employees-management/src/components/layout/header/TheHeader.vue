<template>
  <div class="header-container">
    <div class="header-left">
      <div class="header-left-logo"></div>
      <div class="header-left-company-name">
        <select name="" id="">
          <option value="">
            CÔNG TY TNHH SẢN XUẤT - THƯƠNG MẠI - DỊCH VỤ QUI PHÚC
          </option>
        </select>
      </div>
    </div>
    <div class="header-right">
      <div class="header-right-bell"></div>
      <div class="header-right-avatar"></div>
      <div class="header-right-username">
        <select name="" id="">
          <option value="">Nguyễn Thiện Thắng</option>
        </select>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "TheHeader",
};
</script>

<style scoped>
.header-container {
  height: 56px;
  padding: 0 16px;
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.header-left,
.header-right {
  height: 100%;
  display: flex;
  align-items: center;
}

.header-left-logo {
  background: url("../../assets/img/Sprites.64af8f61.svg") no-repeat -847px -35px;
  width: 16px;
  height: 14px;
  margin-right: 16px;
}

.header-left-company-name select {
  border: none;
  outline: none;
}

.header-right-bell {
  background: url("../../assets/img/Sprites.64af8f61.svg") no-repeat -788px -30px;
  width: 22px;
  height: 25px;
  margin-right: 24px;
}

.header-right-avatar {
  height: 32px;
  width: 32px;
  background: url("../../assets/img/Sprites.64af8f61.svg") no-repeat -352px -894px;
  border-radius: 50%;
}

.header-right-username select {
  border: none;
  outline: none;
}
</style>
