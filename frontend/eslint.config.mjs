import pluginVue from 'eslint-plugin-vue'
import {withVueTs, vueTsConfigs} from '@vue/eslint-config-typescript'
import globals from 'globals'

export default withVueTs(
    {
        ignores: ['dist', 'node_modules', 'coverage'],
    },
    pluginVue.configs['flat/essential'],
    vueTsConfigs.recommended,
    {
        languageOptions: {
            globals: globals.browser,
        },
    },
    {
        files: ['src/components/ui/**/*.vue'],
        rules: {
            // Componentes base do shadcn-vue usam nomes de uma palavra por convenção.
            'vue/multi-word-component-names': 'off',
        },
    },
)
