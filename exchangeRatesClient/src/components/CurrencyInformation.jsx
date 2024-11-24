import { React, useContext, useEffect, useState } from 'react'
import { useParams } from 'react-router-dom';
import NotAvailableCurrency from './NotAvailableCurrency';
import CurrencyExchangeRates from './CurrencyExchangeRates';
import { TailSpin } from 'react-loader-spinner';
import { CurrencyContext } from './CurrentCurrencyProvider';
import { AvailableCurrenciesContext } from './AvailableCurrenciesProvider';

function CurrencyInformation() {

    const { currency, setCurrency } = useContext(CurrencyContext);
    const { availableCurrencies } = useContext(AvailableCurrenciesContext);
    const { currencyName } = useParams();
    const [loading, setLoading] = useState(true);

    function updateCurrency() {
        setCurrency(
            {
                name: currencyName,
                available: availableCurrencies.includes(currencyName)
            });
    }

    useEffect(() => {
        updateCurrency();
    }, [currencyName, availableCurrencies]);

    useEffect(() => {
        if(availableCurrencies==[])
            setLoading(true);
        else
            setLoading(false);
    }, [availableCurrencies])
    return (
        <>
            {loading && (
                <div className="loading-spinner">
                    <TailSpin height="80" width="80" color="blue" ariaLabel="loading" />
                </div>
            )}
            {!loading&&<div className="information">
                {currency && currency.available && <CurrencyExchangeRates />}
                {currency && !currency.available && <NotAvailableCurrency />}
            </div>}
        </>
    )
}

export default CurrencyInformation;
