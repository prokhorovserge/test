<template>
  <div class="container">
    <h1>Прогноз погоды</h1>
    <h2>для {{ latitude }}, {{ longitude }}</h2>
    <h3>Текущий {{ current?.date }} <img :src="current?.conditionIcon" class="condition" /></h3>
    <div class="grid">
      <div class="label">Температура:</div><div class="value">{{ current?.temperature }}°C</div>
      <div class="label">Состояние:</div><div class="value">{{ current?.condition }}</div>
      <div class="label">Ветер:</div><div class="value">{{ current?.windSpeed }} км/ч</div>
      <div class="label">Давление:</div><div class="value">{{ current?.pressure }} мм рт. ст.</div>
    </div>

    <h3>Почасовой</h3>
    <div v-for="(hour, index) in forecastData?.weather.days[0].hours" :key="index">
      <h4 class="hour">{{ hour.date }} <img :src="hour.conditionIcon" class="condition" /></h4>
      <div class="grid">
        <div class="label">Температура:</div><div class="value">{{ hour.temperature }}°C</div>
        <div class="label">Состояние:</div><div class="value">{{ hour.condition }}</div>
        <div class="label">Ветер:</div><div class="value">{{ hour.windSpeed }} км/ч</div>
        <div class="label">Давление:</div><div class="value">{{ hour.pressure }} мм рт. ст.</div>
      </div>
    </div>

    <h3>Прогноз на {{ days }} дня</h3>
    <div v-for="(day, index) in forecastData?.weather.days" :key="index">
      <h4>{{ day.date }} <img :src="day.conditionIcon" class="condition" /></h4>
      <div class="grid">
        <div class="label">Температура:</div><div class="value">{{ day.temperature }}°C</div>
        <div class="label">Состояние:</div><div class="value">{{ day.condition }}</div>
        <div class="label">Ветер:</div><div class="value">{{ day.windSpeed }} км/ч</div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">import { computed } from 'vue'
import { useWeatherApi } from './useWeatherApi'

const {
    getForecast,
    // forecastLoading,
    // forecastError
    forecastData,
} = useWeatherApi()

const latitude = 37.6235989
const longitude = 37.6235989
const days= 3
const forecastParams = {
    latitude,
    longitude,
    days
}

const getData = () => getForecast(forecastParams)

getData()
const current = computed(() => forecastData.value?.weather.current)
</script>

<style scoped>
h3 {
  margin: 40px 0 0;
}
h4 {
  margin: 0 0 0 0;
}
.container {
  justify-content: center;
}
.grid {
  display: grid;
  grid-template-columns: auto auto auto auto;
  margin: 0 auto;
  width: 30%;
}
.label {
  font-weight: bold;
  display: flex;
  justify-content: flex-end;
  margin-left: auto;
  margin-right: 0;
}
.value {
  display: flex;
  justify-content: flex-start;
  margin-left: 10px;
  margin-right: auto;
}
.hour {
  font-weight: bold;
  justify-content: flex-start;
  margin: 0 auto;
  width: 30%;
}
.condition {
  margin: 0, 0, 0, 10px;
  height: 30px;
}
</style>
