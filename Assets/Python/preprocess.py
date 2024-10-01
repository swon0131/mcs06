import pandas as pd
import os

# Folder containing your CSV files
folder_path = r'C:\Users\bryan\OneDrive\Documents\GitHub\mcs06\Assets\FlightCSVData\20 Flights'

# List to hold DataFrames from each modified CSV file
dataframes = []

# Loop through each file in the folder
for file_name in os.listdir(folder_path):
    if file_name.endswith('.csv'):  # Process only CSV files
        file_path = os.path.join(folder_path, file_name)
        
        # Load the CSV file
        data = pd.read_csv(file_path)

        # Split the 'Position' column into 'Latitude' and 'Longitude'
        data[['Latitude', 'Longitude']] = data['Position'].str.split(',', expand=True)

        # Convert the new columns to numeric types
        data['Latitude'] = pd.to_numeric(data['Latitude'])
        data['Longitude'] = pd.to_numeric(data['Longitude'])

        # Drop the original 'Position' column
        data = data.drop(columns=['Position'])

        # Add the modified DataFrame to the list
        dataframes.append(data)

# Concatenate all the DataFrames into one
combined_data = pd.concat(dataframes, ignore_index=True)

# Save the concatenated DataFrame to a new CSV file
combined_output_path = os.path.join(folder_path, 'combined_modified.csv')
combined_data.to_csv(combined_output_path, index=False)

print(f"All modified CSV files have been concatenated and saved to {combined_output_path}")
