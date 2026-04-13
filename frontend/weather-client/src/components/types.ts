export type Location = {
    name: string
    region: string
    country: string
}

export type WeatherItem = {
    date: string
    temperature: number
    condition: string
    conditionIcon: string
    windSpeed: number
    pressure: number
    humidity: number
    cloud: number
}

export type WeatherForecastDay = {
    date: string
    temperature: number
    condition: string
    conditionIcon: string
    windSpeed: number
    pressure: number
    humidity: number
    hours: WeatherItem[]
}

export type WeatherForecast = {
    location: Location
    current: WeatherItem
    days: WeatherForecastDay[]
}

export type GetCurrentWeatherQuery = {
    latitude: number
    longitude: number
}

export type GetCurrentWeatherResult = {
    weather: WeatherItem
}

export type GetWeatherForecastQuery = {
    latitude: number
    longitude: number
    days: number
}

export type GetWeatherForecastResult = {
    forecast: WeatherForecast
}