import { React, useEffect, useContext, useState } from 'react'
import { Outlet } from 'react-router-dom';
import CurrencyInMenu from './CurrencyInMenu';
import { AvailableCurrenciesContext } from './AvailableCurrenciesProvider';
import '../App.css'

function CurrenciesMenu() {
    const currencies = ["USD", "EUR", "GBP", "CNY", "ILS"];
    const { availableCurrencies, setAvailableCurrencies } = useContext(AvailableCurrenciesContext);
    const host = import.meta.env.VITE_HOST;
    const port = import.meta.env.VITE_PORT;
    const [error, setError] = useState(null);

    async function getAvailableCurrencies() {
        try {
            const url = `http://${host}:${port}/api/ExchangeRates/currencies`;
            const response = await fetch(url);
            if (!response.ok) {
                throw new Error(`Error: ${response.status} - ${response.statusText}`);
            }
            const data = await response.json();
            return data;
        } catch (error) {
            setError(`Failed to fetch available currencies: ${error.message}`);
            console.log(error);
            return [];
        }
    }

    useEffect(() => {
        const fetchData = async () => {
            const data = await getAvailableCurrencies();
            setAvailableCurrencies(data);
        };
        fetchData();
    }, [setAvailableCurrencies]);

    function checkIfAvailable(currency) {
        return availableCurrencies.includes(currency);
    }

    return (
        <>
            <div id="showCurrencies">
                <span id="curBtn">currencies</span>
                <div id='currenciesMenu'>
                    {currencies.map((currency, index) => (
                        <CurrencyInMenu key={index} currency={currency} isAvailable={checkIfAvailable(currency)} />
                    ))}
                </div>
                {error && <div style={{ color: 'red', marginTop: '20px' }}>{error}</div>}
            </div>
            <Outlet />
        </>
    )
}

export default CurrenciesMenu;
