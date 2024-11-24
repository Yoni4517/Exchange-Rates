import { React, createContext, useState } from 'react';
export const CurrencyContext = createContext();

export const CurrentCurrencyProvider = ({ children }) => {
     
    const [currency, setCurrency] = useState(null);

    return (
        <CurrencyContext.Provider value={{ currency, setCurrency }}>
            {children}
        </CurrencyContext.Provider>
    );
};
