import pandas as pd

# Load the CSV file
file_path = 'AK6128_3618eb66.csv'
data = pd.read_csv(file_path)

# Split the 'Position' column into 'Latitude' and 'Longitude'
data[['Latitude', 'Longitude']] = data['Position'].str.split(',', expand=True)

# Convert the new columns to numeric types
data['Latitude'] = pd.to_numeric(data['Latitude'])
data['Longitude'] = pd.to_numeric(data['Longitude'])

# Drop the original 'Position' column
data = data.drop(columns=['Position'])

# Save the modified data to a new CSV file
output_file_path = file_path[:len(file_path)-4-1] + "_modified.csv"
data.to_csv(output_file_path, index=False)

print(f"Data has been saved to {output_file_path}")
