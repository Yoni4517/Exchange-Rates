import { React, useContext } from 'react'
import { CurrencyContext } from './CurrentCurrencyProvider';
function NotAvailableCurrency() {
    const { currency } = useContext(CurrencyContext);

    if (!currency || !currency.name) {
        return <div id="notAvailableDiv">Loading...</div>;
    }

    return (
        <div id="notAvailableDiv">
            <h2>The {currency.name} Currency is not Available Now.</h2>
        </div>
    )
}

export default NotAvailableCurrency;
