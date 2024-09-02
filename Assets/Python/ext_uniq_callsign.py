import pandas as pd

# Path to your CSV file
csv_file_path = 'Assets/Python/two_aircraft_data_mod.csv'

# Read the CSV file into a DataFrame
df = pd.read_csv(csv_file_path)

# Extract unique callsigns
unique_callsigns = df['Callsign'].unique()

# Print unique callsigns
for callsign in unique_callsigns:
    print(callsign)
