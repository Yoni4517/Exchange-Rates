import { React } from 'react';
import { useNavigate } from 'react-router-dom';
import '../App.css'

function CurrencyInMenu({ currency, isAvailable }) {

    const navigate = useNavigate();

    return (
        <p className={isAvailable ? "availableCur" : "notAvailableCur"}
            onClick={() => navigate(`currencies/${currency}`)}>
            {currency}
        </p>
    )
}

export default CurrencyInMenu
