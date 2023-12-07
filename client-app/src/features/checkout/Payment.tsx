import { Elements } from '@stripe/react-stripe-js'
import { useEffect, useState } from 'react'
import CheckoutForm from './CheckoutForm'
import { loadStripe } from '@stripe/stripe-js'
import { observer } from 'mobx-react-lite'
import { useStore } from '../../app/stores/store'
import { set } from 'mobx'

export default observer(function Payment() {
  const {checkoutStore, connectionStore} = useStore();
  const {loadConfig, loadClientSecret} = checkoutStore;
  const {selectedConnection} = connectionStore;

	const [stripePromise, setStripePromise] = useState<any>(null)
  const [secret, setSecret] = useState<any>(null)

  useEffect(() => {
    const fetchData = async () => {
      const config = await loadConfig();
      setStripePromise(loadStripe(config!.publishableKey));
    }

    fetchData();
    
  }, []);

  useEffect(() => {
    const fetchData = async () => {
      const clientSecret = await loadClientSecret({
				price: selectedConnection ? selectedConnection.price * 100 : 0,
			});
      setSecret(clientSecret!.clientSecret);
    }

    fetchData();    
  }, []);


	// useEffect(() => {
	// 	fetch('https://localhost:44363/Checkout/config').then(async r => {
	// 		const { publishableKey } = await r.json()
	// 		setStripePromise(loadStripe(publishableKey))
	// 	})
	// }, [])

	// useEffect(() => {
	// 	fetch('https://localhost:44363/Checkout/create-payment-intent', {
	// 		method: 'POST',
	// 		body: JSON.stringify({}),
	// 	}).then(async result => {
	// 		var { clientSecret } = await result.json()
	// 		setClientSecret(clientSecret)
	// 	})
	// }, [])

	return (
		<div className='container'>
			<div className='row'>
				<div className='col-md-4 offset-md-3 border rounded p-4 mt-2 shadow'>
					<h1>Price: {selectedConnection?.price} PLN</h1>
					{secret && stripePromise && (
						<Elements stripe={stripePromise} options={{ clientSecret: secret }}>
							<CheckoutForm />
						</Elements>
					)}
				</div>
			</div>
		</div>
	)
})
