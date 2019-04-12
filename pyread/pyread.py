import pandas as pd
import matplotlib.pyplot as plt
from pathlib import Path
import os
import ta
import seaborn as sns
import numpy as np
#import from the csv into data frame
invested = []

df0file = Path("C:\\Users\\Trappist\\Downloads\\portfolio_holdings.csv")
df1file = Path("C:\\Users\\Trappist\\Downloads\\portfolio_holdings (1).csv")
df2file = Path("C:\\Users\\Trappist\\Downloads\\quotes.csv")
df3file = Path("C:\\Users\\Trappist\\Downloads\\quotes (1).csv")

df0 = pd.read_csv(df0file, names=["Scheme Name","Category","Invested",\
    "Current Value","Units","NAV","Avg NAV","Total Returns","xirr"], \
        delimiter=',', skiprows=1)

invested0 = pd.to_numeric(df0["Invested"]).sum()
currentvalue0 = pd.to_numeric(df0["Current Value"]).sum()

df1 = pd.read_csv(df1file, names=["Scheme Name","Category","Invested",\
    "Current Value","Units","NAV","Avg NAV","Total Returns","xirr"], \
        delimiter=',', skiprows=1)

invested1 = pd.to_numeric(df1["Invested"]).sum()
currentvalue1 = pd.to_numeric(df1["Current Value"]).sum()



df2 = pd.read_csv(df2file, names=["Symbol","Current Price","Date","Time",\
    "Change","Open","High","Low","Volume","Trade Date","Purchase Price",\
        "Quantity","Commission","High Limit","Low Limit","Comment"], \
        delimiter=',', skiprows=1)

invested2 = (pd.to_numeric(df2["Purchase Price"]) * pd.to_numeric(df2["Quantity"])).sum()
currentvalue2 = (pd.to_numeric(df2["Current Price"]) * pd.to_numeric(df2["Quantity"])).sum()

df3 = pd.read_csv(df3file, names=["Symbol","Current Price","Date","Time",\
    "Change","Open","High","Low","Volume","Trade Date","Purchase Price",\
        "Quantity","Commission","High Limit","Low Limit","Comment"], \
        delimiter=',', skiprows=1)

invested3 = (pd.to_numeric(df3["Purchase Price"]) * pd.to_numeric(df3["Quantity"])).sum()
currentvalue3 = (pd.to_numeric(df3["Current Price"]) * pd.to_numeric(df3["Quantity"])).sum()

A =  [
        [invested0,currentvalue0,50],
        [invested1,currentvalue1,50],
        [invested2,currentvalue2,50],
        [invested3,currentvalue3,1],
        [125000,125000,50],
        [950000,1024440,50],              
        [9000,9000,1],
        [1000,1000,1],
        [3300,3300,1],

]

i = sum([item[0] / item[2] for item in A])
c = sum([item[1] / item[2] for item in A])
print("invested: " + str(i))
print("current: " + str(c))
