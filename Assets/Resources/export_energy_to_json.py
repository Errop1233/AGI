import pandas as pd

# Загружаем CSV
df = pd.read_csv("PJME_hourly.csv")

# Преобразуем строку в datetime
df['Datetime'] = pd.to_datetime(df['Datetime'])

# Группируем по дате (только день, без времени)
df['Date'] = df['Datetime'].dt.date
daily = df.groupby('Date')['PJME_MW'].mean().reset_index()

# Сохраняем в JSON
daily.to_json("daily_energy.json", orient="records", indent=2)

print("Файл daily_energy.json успешно создан.")
