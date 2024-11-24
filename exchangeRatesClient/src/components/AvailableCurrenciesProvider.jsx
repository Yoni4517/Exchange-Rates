import { React, createContext, useState } from 'react';
export const AvailableCurrenciesContext = createContext();

export const AvailableCurrenciesProvider = ({ children }) => {
     
    const [availableCurrencies, setAvailableCurrencies] = useState([]);

    return (
        <AvailableCurrenciesContext.Provider value={{ availableCurrencies, setAvailableCurrencies }}>
            {children}
        </AvailableCurrenciesContext.Provider>
    );
};
