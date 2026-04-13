<template>
  <div class="hello">
    <h1>Прогноз погоды</h1>
    <h2>для {{ latitude }}, {{ longitude }}</h2>
    <h3>Текущий</h3>
    <!--
    <ul>
      <li><span class="label">Температура:</span> {{ current.value.temperature }}°C</li>
      <li><span class="label">Состояние:</span> {{ current.value.condition }} <img :src="current.value.conditionIcon" /></li>
    </ul>
    <h3>Прогноз на {{ days }} дня</h3>
    <ul>
      <li><span class="label">Температура:</span> {{ data.value?.weather.days[0].temperature }}°C</li>
      <li><span class="label">Состояние:</span> {{ data.value?.weather.days[0].condition }} <img :src="data.value?.weather.days[0].conditionIcon" /></li>
    </ul>
    -->
  </div>
</template>

<script setup lang="ts">
import { computed } from 'vue'
import { useWeatherApi } from './useWeatherApi'
// import { GetCurrentWeatherQuery, GetCurrentWeatherResult, GetWeatherForecastQuery, GetWeatherForecastResult } from './types'
import { GetWeatherForecastResult } from './types'

// const { getForecast, loading, error } = useWeatherApi()
const { getForecast } = useWeatherApi()

const latitude = 37.6235989
const longitude = 37.6235989
const days= 3
const forecastParams = {
    latitude,
    longitude,
    days
}

const data = computed(() => getForecast<GetWeatherForecastResult>(forecastParams))
// const current = computed(() => data.value?.weather.current.temperature)
</script>

<style scoped>
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
.label {
  font-weight: bold;
}
</style>
