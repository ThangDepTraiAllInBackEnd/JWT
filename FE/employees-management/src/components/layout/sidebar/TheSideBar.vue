<template>
  <div class="sidebar-container">
    <div class="sidebar-header">
      <div class="sidebar-icon"></div>
      <div class="sidebar-logo"></div>
      <div class="sidebar-name">KẾ TOÁN</div>
    </div>

    <router-link to="/employee-list" class="sidebar-option-element">
      <div class="option-element-icon sidebar-general"></div>
      <div class="option-element-text">Tổng quan</div>
    </router-link>

    <router-link to="/employee-cash" class="sidebar-option-element">
      <div class="option-element-icon sidebar-cash"></div>
      <div class="option-element-text">Tiền mặt</div>
    </router-link>

    <router-link to="/employee-money" class="sidebar-option-element">
      <div class="option-element-icon sidebar-money"></div>
      <div class="option-element-text">Tiền gửi</div>
    </router-link>

    <router-link to="/employee-other" class="sidebar-option-element">
      <div class="option-element-icon sidebar-shopping"></div>
      <div class="option-element-text">Mua Hàng</div>
    </router-link>

    <router-link to="/employee-other" class="sidebar-option-element">
      <div class="option-element-icon sidebar-sales"></div>
      <div class="option-element-text">Bán Hàng</div>
    </router-link>

    <router-link to="/employee-other" class="sidebar-option-element">
      <div class="option-element-icon sidebar-bill"></div>
      <div class="option-element-text">Quản lý hóa đơn</div>
    </router-link>

    <router-link to="/employee-other" class="sidebar-option-element">
      <div class="option-element-icon sidebar-stock"></div>
      <div class="option-element-text">Kho</div>
    </router-link>

    <router-link to="/employee-other" class="sidebar-option-element">
      <div class="option-element-icon sidebar-tool"></div>
      <div class="option-element-text">Công cụ dụng cụ</div>
    </router-link>

    <router-link to="/employee-other" class="sidebar-option-element">
      <div class="option-element-icon sidebar-asset"></div>
      <div class="option-element-text">Tài sản cố định</div>
    </router-link>

    <router-link to="/employee-other" class="sidebar-option-element">
      <div class="option-element-icon sidebar-tax"></div>
      <div class="option-element-text">Thuế</div>
    </router-link>

    <router-link to="/employee-other" class="sidebar-option-element">
      <div class="option-element-icon sidebar-price"></div>
      <div class="option-element-text">Giá thành</div>
    </router-link>

    <router-link to="/employee-other" class="sidebar-option-element">
      <div class="option-element-icon sidebar-synthetic"></div>
      <div class="option-element-text">Tổng hợp</div>
    </router-link>

    <router-link to="/employee-other" class="sidebar-option-element">
      <div class="option-element-icon sidebar-budget"></div>
      <div class="option-element-text">Ngân sách</div>
    </router-link>

    <router-link to="/employee-other" class="sidebar-option-element">
      <div class="option-element-icon sidebar-report"></div>
      <div class="option-element-text">Báo cáo</div>
    </router-link>

    <router-link to="/employee-other" class="sidebar-option-element">
      <div class="option-element-icon sidebar-analysis"></div>
      <div class="option-element-text">Phân tích tài chính</div>
    </router-link>
  </div>
</template>

<script>
export default {
  name: "TheSideBar",
};
</script>

<style scoped>
a {
  text-decoration: none;
}
.sidebar-container {
  width: 200px;
  background-color: #041434;
  padding: 0 12px;
}

.sidebar-header {
  display: flex;
  height: 56px;
  align-items: center;
  justify-content: space-around;
}

.sidebar-icon {
  background: url("../../assets/img/Sprites.64af8f61.svg") no-repeat -1157px -32px;
  width: 16px;
  height: 16px;
}

.sidebar-logo {
  background: url("../../assets/img/Sprites.64af8f61.svg") no-repeat -616px -507px;
  background-size: 1008px 953px;
  width: 22px;
  height: 23px;
  scale: 1.5;
  margin-right: -10px;
}

.sidebar-name {
  color: white;
  font-size: 17px;
}

.sidebar-option-element {
  display: flex;
  align-items: center;
  width: 100%;
  height: 44px;
  background-color: #041434;
  color: #869ab8;
}

.sidebar-option-element-target {
  color: #ffff;
  border-radius: 4px;
  background-color: #1e3155;
  cursor: pointer;
}

.sidebar-option-element:hover {
  color: #ffff;
  border-radius: 4px;
  background-color: #1e3155;
  cursor: pointer;
}

.option-element-icon {
  margin: 0 12px;
}

.sidebar-general {
  background: url("../../assets/img/Sprites.64af8f61.svg") no-repeat -33px -1630px;
  width: 20px;
  height: 20px;
}

.sidebar-cash {
  background: url("../../assets/img/Sprites.64af8f61.svg") no-repeat -78px -1631px;
  width: 20px;
  height: 20px;
}

.sidebar-money {
  background: url("../../assets/img/Sprites.64af8f61.svg") no-repeat -121px -1633px;
  width: 20px;
  height: 20px;
}
.sidebar-shopping {
  background: url("../../assets/img/Sprites.64af8f61.svg") no-repeat -165px -1632px;
  width: 20px;
  height: 20px;
}

.sidebar-sales {
  background: url("../../assets/img/Sprites.64af8f61.svg") no-repeat -206px -1631px;
  width: 20px;
  height: 20px;
}
.sidebar-bill {
  background: url("../../assets/img/Sprites.64af8f61.svg") no-repeat -254px -1632px;
  width: 20px;
  height: 20px;
}

.sidebar-stock {
  background: url("../../assets/img/Sprites.64af8f61.svg") no-repeat -294px -1632px;
  width: 20px;
  height: 20px;
}

.sidebar-tool {
  background: url("../../assets/img/Sprites.64af8f61.svg") no-repeat -339px -1633px;
  width: 20px;
  height: 20px;
}

.sidebar-asset {
  background: url("../../assets/img/Sprites.64af8f61.svg") no-repeat -382px -1632px;
  width: 20px;
  height: 20px;
}

.sidebar-tax {
  background: url("../../assets/img/Sprites.64af8f61.svg") no-repeat -427px -1634px;
  width: 20px;
  height: 20px;
}

.sidebar-price {
  background: url("../../assets/img/Sprites.64af8f61.svg") no-repeat -470px -1633px;
  width: 20px;
  height: 20px;
}

.sidebar-synthetic {
  background: url("../../assets/img/Sprites.64af8f61.svg") no-repeat -514px -1632px;
  width: 20px;
  height: 20px;
}

.sidebar-budget {
  background: url("../../assets/img/Sprites.64af8f61.svg") no-repeat -382px -1664px;
  width: 20px;
  height: 20px;
}

.sidebar-report {
  background: url("../../assets/img/Sprites.64af8f61.svg") no-repeat -552px -1636px;
  width: 20px;
  height: 20px;
}

.sidebar-analysis {
  background: url("../../assets/img/Sprites.64af8f61.svg") no-repeat -207px -1667px;
  width: 20px;
  height: 20px;
}
</style>