import React, { useState, useContext, useMemo, useEffect } from 'react';
import { CurrencyContext } from './CurrentCurrencyProvider';
import {
    MaterialReactTable,
    useMaterialReactTable,
} from 'material-react-table';
import '../App.css';

function CurrencyExchangeRates() {

    const host = import.meta.env.VITE_HOST;
    const port = import.meta.env.VITE_PORT;

    const { currency } = useContext(CurrencyContext);
    const [exchangeRates, setExchangeRates] = useState([]);
    const [error, setError] = useState(null); 

    function transformExchangeRates(rates) {
        return Object.keys(rates).map(targetCurrency => ({
            base: currency.name,
            target: targetCurrency,
            exchangeRate: rates[targetCurrency]
        }));
    }

    async function getCurrencyExchangeRates() {
        try {
            const url = `http://${host}:${port}/api/ExchangeRates/currencies/${currency.name}`;
            const response = await fetch(url);

            if (!response.ok) {
                throw new Error(`Error: ${response.status} - ${response.statusText}`);
            }

            const data = await response.json();
            setExchangeRates(transformExchangeRates(data)); 
            setError(null); 

        } catch (error) {
            console.log("Error fetching data:", error); 
            setError(`Failed to fetch exchange rates: ${error.message}`); 
        }
    }

    useEffect(() => {
        getCurrencyExchangeRates(); 
    }, [currency]);

    const columns = useMemo(
        () => [
            {
                accessorKey: 'base',
                header: 'Base',
                size: 150,
            },
            {
                accessorKey: 'target',
                header: 'Target',
                size: 150,
            },
            {
                accessorKey: 'exchangeRate',
                header: 'Exchange Rate',
                size: 150,
            }
        ],
        [],
    );

    const table = useMaterialReactTable({
        columns,
        data: exchangeRates,
    });

    return (
        <div>
            {error && <div style={{ color: 'red', marginBottom: '20px' }}>{error}</div>} 
            <MaterialReactTable table={table} />
        </div>
    );
}

export default CurrencyExchangeRates;
