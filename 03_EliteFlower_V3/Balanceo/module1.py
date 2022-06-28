print("Soy de Python")


    #from csv import excel
import pandas as pd
import sys

name = sys.argv[1]
#otro = sys.argv[2]
#name = "Santiago"


df = pd.read_excel (r'D:\GitHub\EliteFlowerDeploy_V3\DataBases\Prueba_Python.xlsx')
print(df)

print('hola ' + name)
#print(otro)





def Hi(name):
    #from csv import excel
    import pandas as pd

    df = pd.read_excel (r'D:\GitHub\EliteFlowerDeploy_V3\DataBases\Prueba_Python.xlsx')
    print(df)


    print('hola ' + name)
#Hi("santiago")

