import { useState } from 'react';
import { StyleSheet, Text, TextInput, View, Button, SafeAreaView, Alert } from 'react-native';
import { useNavigation } from '@react-navigation/native';

const Login = () => {
    const [ id, setId ] = useState('');
    const [ password, setPassword ] = useState('');

    const navigation = useNavigation();
    
    function login() {
        return ( Alert.alert('Você pressionou o botão de login') );
    }

    return (
        <SafeAreaView style={styles.container}>
            <View style={styles.titleView}>
                <Text style={styles.titleText}>Login</Text>
            </View>

            <View style={styles.formView}>
                <TextInput
                    style={styles.inputText}
                    placeholder='Digite seu prontuário'
                    onChangeText={newText => setId(newText)}
                    />
                <TextInput
                    style={styles.inputText}
                    placeholder='Digite sua senha'
                    onChangeText={newText => setPassword(newText)}
                    secureTextEntry
                />
                <View style={styles.button}>
                    <Button 
                        title='Login'
                        onPress={login}
                    />
                </View>

                <Text
                    style={styles.touchableLabel}
                    onPress={() => navigation.navigate('Register', {})}
                >
                    Clique aqui para registrar uma nova conta
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

export default Login;
