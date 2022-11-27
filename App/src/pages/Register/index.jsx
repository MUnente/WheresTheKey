import { useState } from 'react';
import { SafeAreaView, StyleSheet, Text, TextInput, View, Button, Alert } from 'react-native';
import { useNavigation } from '@react-navigation/native';
import api from '../../services/api';

const Register = () => {
    const [ id, setId ] = useState('');
    const [ name, setName ] = useState('');
    const [ password, setPassword ] = useState('');

    const navigation = useNavigation();

    // methods
    async function register() {
        try {
            const body = { id, name, password };

            console.log(body);
            const response = await api.post('Auth/Register', body)
                .then(function (data) {
                    console.log(response);
                    return Alert.alert("Sucesso");
                })
                .catch(function (error) {
                    console.log('erro do catch certo');
                    console.log(JSON.stringify(error))
                });
        }
        catch (error) {
            return Alert.alert('Falha no login', 'Ocorreu uma falha no login, por favor tente novamente. Se o erro persistir, contate o suporte.');
        }
    };

    return (
        <SafeAreaView style={styles.container}>
            <View style={styles.titleView}>
                <Text style={styles.titleText}>Register</Text>
            </View>
            <View style={styles.formView}>
                <TextInput
                    style={styles.inputText}
                    placeholder='Digite seu prontuário'
                    onChangeText={newText => setId(newText)}
                    />
                <TextInput
                    style={styles.inputText}
                    placeholder='Digite seu nome'
                    onChangeText={newText => setName(newText)}
                    />
                <TextInput
                    style={styles.inputText}
                    placeholder='Digite sua senha'
                    onChangeText={newText => setPassword(newText)}
                    secureTextEntry
                    />
                <View style={styles.button}>
                    <Button 
                        title='Registrar'
                        onPress={register}
                    />
                </View>
                <Text style={styles.touchableLabel} onPress={() => navigation.goBack()}>
                    Voltar para o login
                </Text>
            </View>
        </SafeAreaView>
    );
}
  
const styles = StyleSheet.create({
    container: {
        flex: 1
    },
    titleView: {
        justifyContent: 'center',
        alignItems: 'center',
        marginBottom: 50
    },
    titleText: {
        fontSize: 32,
        fontWeight: "bold",
        marginBottom: 20
    },
    formView: {
        justifyContent: 'center',
        alignItems: 'center'
    },
    inputText: {
        fontSize: 16,
        padding: 10,
        height: 50,
        width: 250,
        borderColor: "grey",
        borderWidth: 1
    },
    button: {
        marginTop: 20,
        width: 150
    },
    touchableLabel: {
        marginTop: 20,
        textDecorationLine: 'underline'
    }
});

export default Register;
